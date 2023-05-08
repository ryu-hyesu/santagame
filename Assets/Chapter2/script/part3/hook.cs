using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class hook : MonoBehaviour
{
    [Header("NPC position")]
    public Transform[] chatTr;
    
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [SerializeField] GameObject crco;
    Transform crcoFirst;
    [SerializeField] Transform crcoEnd;


    private bool playerInRange;
    public bool isDone;
    public bool isPlaying;
    public GameObject player;
    //카메라 전환
    public CinemachineVirtualCamera vCam1;
    public CinemachineVirtualCamera vCam2;
    public CinemachineVirtualCamera vCam3;
    //속도
    public float blockMoveSpeed;
    
    //end
    public bool finished = false;
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
                StartCoroutine(gotoone1());
            }
        }
    }
    
    bool first = true;
    private IEnumerator gotoone1(){
        gameVariable.isTalk = true;
        
        yield return new WaitUntil(()=> ChatSystem.GetInstance().upup(inkJSON, chatTr));
        yield return new WaitForSeconds(1.0f);

        vCam2.Priority = 12;

        yield return new WaitForSeconds(2.0f);
        Vector3 dir = Vector3.right;

        while (Vector3.Magnitude(crcoEnd.position - crco.transform.position) >= 0.01f)
        {           
            crco.transform.Translate(dir * Time.deltaTime * blockMoveSpeed);   
            yield return null;
        }

        crco.transform.position = crcoEnd.position;

        // var runTime =0.0f;

        // while (runTime<3f)
        // {
        //     runTime += Time.deltaTime;

        //     crco.transform.position = Vector3.MoveTowards(crcoFirst.position, crcoEnd.position, runTime / 20f);
            
        //     yield return null;
        // }

        vCam3.Priority = 13;

        yield return new WaitForSeconds(2.0f);

        player.GetComponent<SpriteRenderer>().flipX = false;

        yield return new WaitUntil(()=> ChatSystem.GetInstance().upup(inkJSON, chatTr));

        ShipVariable.isShow = false;

        SceneManager.LoadScene("croc");

        finished = true;

        gameVariable.isGame = true;
        gameVariable.isTalk = false;
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
