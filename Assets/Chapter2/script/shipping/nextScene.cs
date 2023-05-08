using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playerInRange;
    public senceType sencetype;
    //fade out
    public GameObject panelobject;
    private CanvasGroup cg;
    public float fadeTime = 1f; // 페이드 타임 
    float accumTime = 0f;
    private Coroutine fadeCor;
    // 이전에 미리 켜져야 할 트리거
    [SerializeField]
    GameObject FinshNPC2;
    bool isTrigger;
    void Start()
    {
        cg = panelobject.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && !gameVariable.isTalk && ((FinshNPC2 != null) ? FinshNPC2.GetComponent<npcTrigger>().finished : true)){
            if(Input.GetKeyDown(KeyCode.X)){
                StartCoroutine(gotoone1());
            }
        }
    }

    public void retrunSecene(){
        switch(sencetype){
            case senceType.inShip:
                SceneManager.LoadScene("cake"); 
                break;
            case senceType.cakeGame:
                SceneManager.LoadScene("cake"); 
                break;
            case senceType.crocoGame:
                SceneManager.LoadScene("croc"); 
                break;
            case senceType.puzzleGame:
                SceneManager.LoadScene("puzzle"); 
                break;
            case senceType.lastShip:
                SceneManager.LoadScene("SHIP"); 
                break;
        }    
    }

    IEnumerator gotoone1(){
        gameVariable.isTalk = true;

        accumTime = 0f;
        while (accumTime < fadeTime) 
        {
            cg.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 1f;

        gameVariable.isTalk = false;
        retrunSecene();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Debug.Log("d");
            playerInRange = true; 
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            playerInRange = false;
        }
    }
}
