using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnStartFadeOut : MonoBehaviour
{
    public Image fadeImage; //시작 시 fade out

    void Start()
    {
        //시작 시 fade out
        if (fadeImage.color == new Color(0, 0, 0, 1))
        {
            StartCoroutine("FadeOut");
        }
    }
    IEnumerator FadeOut()
    {
        float f;
        for (f = 1f; f >= 0f; f -= 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            fadeImage.color = new Color(0, 0, 0, f);
        }
    }
}
