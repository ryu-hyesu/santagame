using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountItem : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    public Text playerCountText;


    public void GetItem(int count)
    {
        playerCountText.text = count.ToString();
    }

    [Header("NPC position")]
    public Transform[] chatTr;
    
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;
    public bool isDone;
    public bool isPlaying;

    [SerializeField] 
    Camera maincamera;
/*
    [SerializeField] 
    float _amount;
    [SerializeField]
    float _duration;*/

    public float shakeTimer =0; //흔들림 효과 시간
        public float shakeAmount; //흔들림 범위
    
    //end
    // Start is called before the first frame update
    void Start()
    {
        //finished = player.GetComponent<PlayerMove>().isEnd;
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && !gameVariable.isTalk && player.GetComponent<PlayerMove>().isEnd){
            
                isDone = true;
                StartCoroutine(gotoone1());
            
        }
    }

    public float ShakeAmount;
    float ShakeTime;
    Vector3 initialPosition;
    public void VibrateForTime(float time)
    {
        ShakeTime = time;
    }
    
    bool first = true;
    private IEnumerator gotoone1(){
        gameVariable.isTalk = true;
        yield return new WaitForSeconds(1);
        
        yield return new WaitUntil(()=> ChatSystem.GetInstance().upup(inkJSON, chatTr));

        maincamera.GetComponent<cameraController>().enabled = false;

        VibrateForTime(5);

        Vector3 initialPosition = GameObject.FindWithTag("MainCamera").transform.position;//카메라 흔들릴 위치값
        if (ShakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0.0f;
            transform.position = initialPosition;
        }
/*
        float timer=0;
        while(timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + maincamera.transform.position;
    
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = maincamera.transform.position;
        */

        maincamera.GetComponent<cameraController>().enabled = true;
        gameVariable.isTalk = false;
        Destroy(this);
    }

    public IEnumerator Shake(float _amount,float _duration)
    {
        float timer=0;
        while(timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + maincamera.transform.position;
    
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = maincamera.transform.position;
    
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
}