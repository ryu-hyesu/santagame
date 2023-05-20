using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class secondTrigger : MonoBehaviour
{
    [Header("NPC position")]
    public Transform[] chatTr;
    [Header("new NPC position")]
    public Transform[] newPosition;
    [Header("new NPC")]
    public GameObject[] npcs;
    
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public bool playerInRange;
    public bool isDone;
    public bool isPlaying;
    //end
    public bool finished = false;
    AudioSource audioSoure;
    // 위치
    public GameObject player;
    public GameObject playerOnShip;
    public GameObject hook;
    //카메라 전환
    public CinemachineVirtualCamera vCam1;
    public CinemachineVirtualCamera vCam2;
    public CinemachineVirtualCamera vCam3;
    public Camera mainCamera;

    //fade out
    public GameObject panelobject;
    private CanvasGroup cg;
    public float fadeTime = 1f; // 페이드 타임 
    float accumTime = 0f;
    private Coroutine fadeCor;
    void Start()
    {
        audioSoure = GetComponent<AudioSource>();
        playerInRange = false;
        cg = panelobject.GetComponent<CanvasGroup>();
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

    // 새 postition
    private void newPositionSetting(){
        npcs[0].transform.position += new Vector3(2, -0.5f, 0);
        npcs[1].transform.position += new Vector3(2, -0.7f, 0);
    }

    bool first = true;
    private IEnumerator gotoone1(){
        gameVariable.isTalk = true;

        vCam1.Priority = 0;
        vCam2.Priority = 11;

        yield return new WaitForSeconds(2.0f);
        audioSoure.Play();
        yield return new WaitUntil(()=> ChatSystem.GetInstance().upup(inkJSON, chatTr));

        // FADE IN & OUT
        yield return new WaitForSeconds(0.2f);
        accumTime = 0f;
        while (accumTime < fadeTime) 
        {
            cg.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 1f;

        // 산타 위치 이동
        playerOnShip.SetActive(false);
        player.SetActive(true);

        //npc 자리 이동
        newPositionSetting();
        hook.GetComponent<SpriteRenderer>().flipX = false;
        hook.transform.position += new Vector3(2, 0, 0);

        // 카메라 변경
        vCam2.Priority = 0;
        vCam3.Priority = 12;

        yield return new WaitForSeconds(2.0f);
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 0f;

        yield return new WaitForSeconds(2.0f);

        yield return new WaitUntil(()=> ChatSystem.GetInstance().upup(inkJSON, chatTr));

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
