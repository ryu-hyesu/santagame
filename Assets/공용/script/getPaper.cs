using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getPaper : MonoBehaviour
{
    bool playerInRange = false;
    bool getItem = false;   //아이템 줍기

    public GameObject item; //책장 아이템
    public GameObject player;   //아이템 습득 시 플레이어 머리 위로 떠오르게 함
    public GameObject point;
    Vector3 pos;

    public GameObject chapterImg;   //일러스트 활성화
    Image chImg; //일러스트 투명도
    public GameObject textBackground;   //흰색 투명
    Image txtImg;
    public Text text;

    public GameObject fade; //fade 활성화
    Image fadeImg;  //fade
    public GameObject bookStartImg;
    public GameObject book; //책장 넘어가는 애니메이션

    Color c;

    int step=0;

    void Start()
    {
        pos = new Vector3(item.GetComponent<Transform>().position.x, point.GetComponent<Transform>().position.y, item.GetComponent<Transform>().position.z);
        chImg = chapterImg.GetComponent<Image>();
        txtImg = textBackground.GetComponent<Image>();
        fadeImg = fade.GetComponent<Image>();

        
        getItem = false;
    }

    void Update()
    {
        if(playerInRange){
            if(Input.GetKeyDown(KeyCode.X)){
                getItem = true;
            }
        }

        if(getItem){
            item.GetComponent<Transform>().position = Vector3.MoveTowards(item.GetComponent<Transform>().position, pos, 2f*Time.deltaTime);
        }
        if(getItem && item.GetComponent<Transform>().position==pos){
            getItem = false;
            chapterImg.SetActive(true);
            StartCoroutine(FadeIn(chImg, 1));
            //StartCoroutine(FadeTextToFullAlpha());
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            playerInRange = false;
        }
    }

    public IEnumerator FadeTextToFullAlpha() // 알파값 0에서 1로 전환
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator FadeIn(Image img, float end)
    {
        //Debug.Log("fadeIn "+step);
        float f=0;

        if(step==1){
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

            for(f = 0.05f;f<=end;f+=0.05f){
                yield return new WaitForSeconds(0.05f);
                text.color = new Color(text.color.r, text.color.g, text.color.b, f);
                img.color = new Color(255,255,255,f);
            }
        } else{
            for(f = 0.05f;f<=end;f+=0.05f){
                yield return new WaitForSeconds(0.05f);
                img.color = new Color(255,255,255,f);
            }
        }
        
        if(f >= end){
            yield return new WaitForSeconds(2f);
            if(step==0){
                step += 1;
                textBackground.SetActive(true);
                StartCoroutine(FadeIn(txtImg, 0.8f));
            } else if(step==1){
                step += 1;
                fade.SetActive(true);
                StartCoroutine(FadeIn(fadeImg, 1));
            } else if(step ==2){
                step += 1;
                bookStartImg.SetActive(true);
                StartCoroutine("FadeOut");
            }
            
        }
    }
    
    IEnumerator FadeOut()
    {
        float f=1;
        fadeImg.color = new Color(0,0,0,f);
        
        for(;f>=0;f-=0.05f){
            yield return new WaitForSeconds(0.05f);
            fadeImg.color = new Color(0,0,0,f);
        }
        if(f <= 0){
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(1f);
            //fade.SetActive(false);
            book.SetActive(true);
        }
    }
}
