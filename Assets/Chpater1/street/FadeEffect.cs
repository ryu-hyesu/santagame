using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public Image image;
    public bool isDone;
    public static FadeEffect instance;

    // Start is called before the first frame update
    void Start()
    {
        if (image.color == new Color(0, 0, 0, 1))
        {
            StartCoroutine("FadeOut");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ChatSystem.GetInstance().isSceneChanged && !isDone)
        {
            isDone = true;
            StartCoroutine("FadeIn");
        }
    }

    IEnumerator FadeIn()
    {
        float f;
        for (f = 0f; f <= 1; f += 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0, 0, 0, f);
        }
        SceneManager.LoadScene("1_livingroom");
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
