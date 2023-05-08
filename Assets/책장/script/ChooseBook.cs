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

    // Start is called before the first frame update
    void Start()
    {   
        isEnd = false;
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
        if (Input.GetMouseButtonUp(0))
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
                        bookCover.SetActive(false);
                        image.color = new Color(0, 0, 0, 0f);
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
