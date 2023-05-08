using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class firstDial : MonoBehaviour
{
    [Header("NPC position")]
    public Transform[] chatTr;
    
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [Header("camera")]
    public Camera firstPersonCamera;
    public Camera overheadCamera;

    private bool playerInRange;
    public bool isDone;
    public bool isPlaying;
    public GameObject game;
    public float time;
    public Image image;
    public GameObject player;
    public GameObject shipPlayer;
    public Transform playerPos;
    public Transform cameraPos;

    [Header("ship")]
    public GameObject ship;

     
    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && !gameVariable.isTalk){
            if(Input.GetKeyDown(KeyCode.X)){
                isDone = true;
                
                ship.GetComponent<MovingShip>().enabled = true;
                Rigidbody2D ri = ship.GetComponent<Rigidbody2D>();
                Destroy(ri);


                StartCoroutine(gotoone());
            }
        }
    }

    

    private IEnumerator gotoone(){
        gameVariable.isTalk = true;
        
        firstPersonCamera.GetComponent<cameraController>().enabled = false;
        float plus = 0;
        while(plus < 0.3){

                Camera.main.transform.position +=  new Vector3(plus,0,0);
                plus += 0.008f;
                yield return new WaitForSeconds(0.01f);
        }
        /*
        yield return new WaitUntil(()=> ChatSystem.GetInstance().upup(inkJSON, chatTr));
        
        float fadeCount = 0;
        while(fadeCount < 1.0f){
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0,0,0,fadeCount);
        }

        shipPlayer.SetActive(false);
        player.GetComponent<Transform>().transform.position = playerPos.position;
        firstPersonCamera.GetComponent<cameraController>().enabled = true;

        fadeCount = 1;
        while(fadeCount > 0){
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0,0,0,fadeCount);
        }
        //sliderCanvas.SetActive(true);

        
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(()=> ChatSystem.GetInstance().upup(inkJSON, chatTr));
        
        
        */


        //gameVariable.noMove = false;
        gameVariable.isTalk = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Debug.Log("dd");
            playerInRange = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            playerInRange = false;
        }
    }

}
