using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ch1_SceneManager : MonoBehaviour
{
    public Image image;
    Scene scene;
    bool isBlack;

    void Start()
    {
        isBlack = false;

        if (image.color == new Color(0, 0, 0, 1))
        {
            StartCoroutine("FadeOut");
        }
    }
    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }

    //ch1 계단
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "nextScene" && Input.GetKeyDown(KeyCode.X) && !isBlack)
        {
            isBlack = true;
            Debug.Log("x 입력");

            if (scene.name == "1_livingroom")
            {
                StartCoroutine("FadeIn");
                StartCoroutine(loadScene1());
            }
            if (scene.name == "2_hallway1F")
            {
                StartCoroutine("FadeIn");
                StartCoroutine(loadScene2());
            }
            if (scene.name == "3_hallway2F")
            {
                StartCoroutine("FadeIn");
                StartCoroutine(loadScene3());
            }
            if (scene.name == "6_children")
            {
                StartCoroutine("FadeIn");
                StartCoroutine(loadScene4());
            }
            if (scene.name == "7_hallway2F")
            {
                StartCoroutine("FadeIn");
                StartCoroutine(loadScene5());
            }
            if (scene.name == "8_hallway1F")
            {
                StartCoroutine("FadeIn");
                StartCoroutine(loadScene6());
            }
            if (scene.name == "9_livingroom")
            {
                StartCoroutine("FadeIn");
                StartCoroutine(loadScene7());
            }
            if (scene.name == "10_hallway1F")
            {
                StartCoroutine("FadeIn");
                StartCoroutine(loadScene8());
            }
            //ChatSystem2에서 코드
            /*if (scene.name == "11_kitchen")
            {
                StartCoroutine("FadeIn");
                StartCoroutine(loadScene9());
            }*/
            if (scene.name == "13_kitchen")
            {
                StartCoroutine("FadeIn");
                StartCoroutine(loadScene10());
            }
            if (scene.name == "14_hallway1F")
            {
                StartCoroutine("FadeIn");
                StartCoroutine(loadScene11());
            }
            if (scene.name == "15_hallway2F")
            {
                StartCoroutine("FadeIn");
                StartCoroutine(loadScene12());
            }

            //16 -> ch2로 이동: ChatSystem2에
        }

    }
    IEnumerator loadScene1()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("2_hallway1F");
    }
    IEnumerator loadScene2()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("3_hallway2F");
    }
    IEnumerator loadScene3()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("4_children");
    }
    IEnumerator loadScene4()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("7_hallway2F");
    }
    IEnumerator loadScene5()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("8_hallway1F");
    }
    IEnumerator loadScene6()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("9_livingroom");
    }
    IEnumerator loadScene7()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("10_hallway1F");
    }
    IEnumerator loadScene8()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("11_kitchen");
    }
    IEnumerator loadScene9()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("12_game2");
    }
    IEnumerator loadScene10()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("14_hallway1F");
    }
    IEnumerator loadScene11()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("15_hallway2F");
    }
    IEnumerator loadScene12()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("16_children");
    }

    IEnumerator FadeIn()
    {
        float f;
        for (f = 0f; f <= 1; f += 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0, 0, 0, f);
        }
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
