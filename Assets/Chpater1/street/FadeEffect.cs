using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public Image image;
    public bool isDone;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameVariable.isGame);
        Debug.Log(gameVariable.isTalk);
        Debug.Log(gameVariable.noMove);
        Debug.Log(Time.timeScale);
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
        SceneManager.LoadScene("House1_0");    //���� �������
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
