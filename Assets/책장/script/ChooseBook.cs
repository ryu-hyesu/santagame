using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseBook : MonoBehaviour
{
    public Image image;
    public Animator anim;
    bool isClicked;
    bool isEnd;

    public GameObject warning;
    public GameObject bookCover;

    // book cover 클릭 후 다음 장면 넘어가는 bool 변수
    bool isBOOK;

    // Start is called before the first frame update
    void Start()
    {   
        isEnd = false;
        isBOOK = false;
        anim = GetComponent<Animator>();
        anim.SetBool("isBigger", true);
        if (image.color == new Color(0, 0, 0, 1))
        {
            StartCoroutine("FadeOut");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !isBOOK)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.name == "book")
                {
                    isClicked = true;
                    warning.SetActive(false);
                    image.color = new Color(0, 0, 0, 0.5f);
                    bookCover.SetActive(true);

                }
                else if (isClicked){
                    if(!isEnd){
                        isClicked = false;
                        isBOOK = true;
                        StartCoroutine("next");
                    }
                }
                else if (hit.collider.gameObject.layer.Equals(LayerMask.NameToLayer("otherBook")) && !isClicked)
                {
                    Debug.Log(hit.collider.name);
                    warning.SetActive(true);
                    Invoke("hideWaring",2f);
                }
                else if (hit.collider.name == "bookCover" && !isEnd)
                {
                    isEnd = true;
                    Invoke("delayFadeIn", 3);
                }
                
            }
            else{
                if(!isEnd){
                    isClicked = false;
                    bookCover.SetActive(false);
                    image.color = new Color(0, 0, 0, 0f);
                }
            }
        }
    }

    private void hideWaring(){
        warning.SetActive(false);
    }

    void delayFadeIn()
    {
        StartCoroutine("FadeIn");
    }

    public float animTime = 2f;
    private float start = 1f;           // Mathf.Lerp 메소드의 첫번째 값.  
    private float end = 0f;             // Mathf.Lerp 메소드의 두번째 값.  
    private float time = 0f; 

    IEnumerator next(){
        bookCover.SetActive(false);

        Color color = image.color;  
        time = 0f;  
        color.a = Mathf.Lerp(end, start, time);  

        while (color.a < 1f)  
        {  
            // 경과 시간 계산.  
            // 2초(animTime)동안 재생될 수 있도록 animTime으로 나누기.  
            time += Time.deltaTime / animTime;  

            // 알파 값 계산.  
            color.a = Mathf.Lerp(end, start, time);  
            // 계산한 알파 값 다시 설정.  
            image.color = color;  

            yield return null;  
        }  

        yield return null;
        SceneManager.LoadScene("prolog");
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
