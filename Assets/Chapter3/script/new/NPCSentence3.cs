using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCSentence3 : MonoBehaviour
{
    AudioSource audioSource;

    public Image image;
    Scene scene;

    public TextAsset[] inkJSON;
    public Transform[] chatTr;
    public GameObject[] showNPC;
    private bool playerInRange;
    public Transform targetPoint;

    public float moveSpeed = 5f;
    public int startTalking = 0;
    public bool did = false;
    bool flag = false;

    public AudioClip audioTalk;
    public AudioClip audioFinish;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void Start()
    {
        playerInRange = false;
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if(scene.name == "ch3_dialogue"){
            if(showNPC[0].activeSelf == true){
                if(showNPC[0].GetComponent<Transform>().position == targetPoint.position){
                    //anim.SetBool("isWalking", false);
                    if(startTalking == 1)
                        flag = true;
                }else{
                    showNPC[0].GetComponent<Transform>().position = Vector3.MoveTowards(showNPC[0].GetComponent<Transform>().position, targetPoint.position, moveSpeed*Time.deltaTime);
                }
            }
            Debug.Log(startTalking);
            if(playerInRange && !ChatSystem.GetInstance().dialogueIsPlaying){
                if(!did){
                    if(startTalking== 1){
                        for(int i=0;i<showNPC.Length;i++)
                            showNPC[i].SetActive(true);
                        if(flag){
                            Debug.Log("n1"+flag);
                            ChatSystem.GetInstance().Ondialogue(inkJSON[startTalking], chatTr);
                            //startTalking = 2;
                            Invoke("setTalkingNum", 3);
                        }
                    }else if(startTalking == 2){
                        Debug.Log("n2" +flag);
                        if(flag){
                            startFading();
                            flag = false;
                        }
                    }else if(Input.GetKeyDown(KeyCode.X)){
                        PlaySound("Talk");
                        Debug.Log("n0"+flag);
                        ChatSystem.GetInstance().Ondialogue(inkJSON[startTalking], chatTr);
                        Invoke("setTalkingNum", 3);
                    }
                }
            }

            if(startTalking == 3 && !flag){
                Invoke("endTalk", 2);
                flag = true;
            }
        }else if(scene.name =="ch3_game"){
            Debug.Log("play game");
        }
        
    }

    void endTalk(){
        ChatSystem.GetInstance().destroy();
        startFading();
    }

    void PlaySound(string action){
        switch(action){
            case "Talk":
                audioSource.clip = audioTalk;
                break;
            case "Finish":
                audioSource.clip = audioFinish;
                break;
        }
        audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            playerInRange = false;
        }
    }
    void setTalkingNum()
    {
        startTalking+=1;
    }

    IEnumerator FadeOut()
    {
        for(int i=1;i<showNPC.Length;i++){
            Color color = showNPC[i].GetComponent<SpriteRenderer>().color;
            color.a = 0f;
            showNPC[i].GetComponent<SpriteRenderer>().color = color;
        }

        float f;
        for(f = 1f;f>=0f;f-=0.05f){
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0,0,0,f);
        }
        if(f<=0){
            ChatSystem.GetInstance().Ondialogue(inkJSON[2], chatTr);
            did=true;
        }
        Invoke("setTalkingNum", 3);
    }

    IEnumerator FadeIn()
    {
        float f;
        for(f = 0.05f;f<=1;f+=0.05f){
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0,0,0,f);
        }
        if(f >= 1){
            Debug.Log("FadeOut");
            if(startTalking==3){
                PlaySound("Finish");
                yield return new WaitForSeconds(0.05f);
                SceneManager.LoadScene("game4");
            }else
                StartCoroutine("FadeOut");
        }
    }

    public void startFading()
    {
        Debug.Log("FadeIn");
        //ChatSystem.GetInstance().destroy();
        StartCoroutine("FadeIn");
    }
}