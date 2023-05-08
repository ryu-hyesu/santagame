using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    //부제
    public GameObject guide;
    private CanvasGroup cg;
    float fadeTime = 3f;

    void Start()
    {
        cg = guide.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FadingCG(){
        StartCoroutine("FadeIn");
    }

    private IEnumerator FadeIn(){
        yield return new WaitForSeconds(3.0f);
        float accumTime = 0f;
        while (accumTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 0f;
    }
}
