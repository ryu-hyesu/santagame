using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game1Start : MonoBehaviour
{
    public Image image;

    void Start()
    {
        if (image.color == new Color(0, 0, 0, 1))
        {
            StartCoroutine("FadeOut");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        float f;
        for (f = 0f; f <= 1; f += 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0, 0, 0, f);
        }
        SceneManager.LoadScene("5_game1");    //findItem game scene
    }

    IEnumerator FadeOut()
    {
        float f;
        for (f = 1f; f >= 0f; f -= 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0, 0, 0, f);
        }
    }
}
