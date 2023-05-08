using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FindItem : MonoBehaviour
{
    SpriteRenderer sr;
    public GameObject itemBox;

    public Image image;
    public int cnt;
    public float itemNum;
    bool endState = false;

    //효과음
    AudioSource audioSoure;

    void Start()
    {
        if (image.color == new Color(0, 0, 0, 1))
        {
            StartCoroutine("FadeOut");
        }
        sr = itemBox.GetComponent<SpriteRenderer>();
        
        Color c = sr.material.color;
        c.a = 0;
        sr.material.color = c;

        audioSoure = GetComponent<AudioSource>();
    }
    void Update()
    {
        //guide off 후 start
        Invoke("gameStart", 4);
    }
    public void gameStart()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(pos, Vector2.zero);
            float distance = Mathf.Infinity;

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distance, 1 << LayerMask.NameToLayer("Item"));

            if (hit.collider != null)
            {
                audioSoure.Play();
                GameObject clickObj = hit.transform.gameObject;
                Debug.Log(clickObj.name);
                Destroy(clickObj);
                cnt++;
            }
        }

        if (cnt == itemNum && !endState)
        {
            endState = true;
            Debug.Log("퀘스트 완료!!");

            StartCoroutine("showItemBoxImage");
        }
    }

    IEnumerator showItemBoxImage()
    {
        for (int i = 0; i < 10; i++)
        {
            float f = i / 10.0f;
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
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
        SceneManager.LoadScene("6_children");
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
