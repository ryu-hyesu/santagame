using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class starting : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("NPC position")]
    public Transform[] chatTr;
    
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public bool playerInRange;
    public bool isDone;
    public bool isPlaying;
    //end
    public bool finished = false;
    AudioSource audioSoure;
    // Start is called before the first frame update

    //카메라 전환
    public CinemachineVirtualCamera vCam1;
    public CinemachineVirtualCamera vCam2;

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
        if (playerInRange == false)
            StartCoroutine(gotoone1());
        
    }
    
    bool first = true;
    private IEnumerator gotoone1(){
        gameVariable.isTalk = true;
        playerInRange = true;

        yield return new WaitForSeconds(3.0f);
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 0f;

        yield return new WaitForSeconds(2.0f);

        vCam1.Priority = 9;
        vCam2.Priority = 10;

        yield return null;
        
        yield return new WaitUntil(()=> ChatSystem.GetInstance().upup(inkJSON, chatTr));

        yield return new WaitForSeconds(2.0f);

        finished = true;
        
        gameVariable.isGame = true;
        gameVariable.isTalk = false;
        vCam1.Priority = 10;
        vCam2.Priority = 9;
    }

    public void StartFadeIn() // 호출 함수 Fade In을 시작
    {
        if (fadeCor != null)
        {
            StopAllCoroutines();
            fadeCor = null;
        }
        fadeCor = StartCoroutine(FadeOut());
    }

   private IEnumerator FadeIn() // 코루틴을 통해 페이드 인 시간 조절
    {
        yield return new WaitForSeconds(0.2f);
        accumTime = 0f;
        while (accumTime < fadeTime) 
        {
            cg.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 1f;

        StartCoroutine(FadeOut()); //일정시간 켜졌다 꺼지도록 Fade out 코루틴 호출

    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3.0f);
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 0f;
    }

    
}
