using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showTG : MonoBehaviour
{
    public GameObject Title;
    public GameObject guide;
    public Text guideText;
    private CanvasGroup titleGroup;
    private CanvasGroup guideGroup;
    


    private float accumTime;
    private float fadeTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        titleGroup = Title.GetComponent<CanvasGroup>();
        guideGroup = guide.GetComponent<CanvasGroup>();
        StartCoroutine(gotoone1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator gotoone1(){
        gameVariable.isTalk = true;

        yield return new WaitForSeconds(3.0f);
        accumTime = 0f;

        while (accumTime < fadeTime)
        {
            titleGroup.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        titleGroup.alpha = 0f;

        yield return null;

        accumTime = 0f;


        guideText.text = "단서를 찾아 방을 탈출하자!";

        yield return new WaitForSeconds(3.0f);

        

        while (accumTime < fadeTime)
        {
            guideGroup.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        guideGroup.alpha = 0f;

        gameVariable.isTalk = false;
    }
}
