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

    public GameObject fade; //fade 활성화
    Image fadeImg;  //fade
    public GameObject bookStartImg;
    public GameObject book; //책장 넘어가는 애니메이션

    Color c;

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
            StartCoroutine("FadeIn");
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
    /*
    IEnumerator FadeIn()
    {
        float f=0;
        for(f = 0.05f;f<=1;f+=0.05f){
            yield return new WaitForSeconds(0.05f);
            img.color = new Color(255,255,255,f);
        }
        if(f >= 1){
            cnt+=1;
            yield return new WaitForSeconds(2f);
            activeObj.SetActive(true);

            if(cnt<=3)
                StartCoroutine("FadeIn");
            else
                StartCoroutine("FadeOut");
        }
    }
    */
    
    IEnumerator FadeIn()
    {
        float f=0;
        for(f = 0.05f;f<=1;f+=0.05f){
            yield return new WaitForSeconds(0.05f);
            chImg.color = new Color(255,255,255,f);
        }
        if(f >= 1){
            yield return new WaitForSeconds(2f);
            textBackground.SetActive(true);
            StartCoroutine("FadeIn2");
        }
    }

    IEnumerator FadeIn2()
    {
        float f=0;
        for(f = 0.05f;f<=0.8;f+=0.05f){
            yield return new WaitForSeconds(0.05f);
            txtImg.color = new Color(255,255,255,f);
        }
        if(f >= 0.7){
            yield return new WaitForSeconds(2f);
            fade.SetActive(true);
            StartCoroutine("FadeIn3");
        }
    }

    IEnumerator FadeIn3()
    {
        float f=0;
        for(f = 0.05f;f<=1;f+=0.05f){
            yield return new WaitForSeconds(0.05f);
            fadeImg.color = new Color(255,255,255,f);
        }
        if(f >= 1){
            yield return new WaitForSeconds(0.05f);
            bookStartImg.SetActive(true);
            StartCoroutine("FadeOut");
        }
    }
    
    IEnumerator FadeOut()
    {
        float f=1;
        fadeImg.color = new Color(255,255,255,f);
        
        for(;f>=0;f-=0.05f){
            yield return new WaitForSeconds(0.05f);
            fadeImg.color = new Color(255,255,255,f);
        }
        if(f <= 0){
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(1f);
            book.SetActive(true);
        }
    }
}
