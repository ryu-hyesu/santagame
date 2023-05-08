using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCh : MonoBehaviour
{
    public PlayerMove playerMove;
    public bool playerInRange = false;
    public GameObject item;
    SpriteRenderer sr;
    public GameObject chapterImg;
    public GameObject book;
    Vector3 pos;
    Color c;

    bool isDone;

    void Start()
    {
        isDone = false;
        pos = new Vector3(item.GetComponent<Transform>().position.x, Mathf.Abs(item.GetComponent<Transform>().position.y)/2, item.GetComponent<Transform>().position.z);

        
    }

    void Update()
    {
        if(playerInRange && !gameVariable.isTalk){
            if(Input.GetKeyDown(KeyCode.X)){
                isDone = true;
            }
        }

        if(isDone){
            item.GetComponent<Transform>().position = Vector3.MoveTowards(item.GetComponent<Transform>().position, pos, 2f*Time.deltaTime);
        }
        if(isDone && item.GetComponent<Transform>().position==pos){
            isDone = false;
            chapterImg.SetActive(true);
            sr = chapterImg.GetComponent<SpriteRenderer>();
            startFading();
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

    IEnumerator FadeIn()
    {
        float f;
        for(f = 0.05f;f<=1;f+=0.05f){
            c = sr.material.color;
            c.a = f;

            sr.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        if(f >= 1){
            book.SetActive(true);
        }
    }

    void startFading()
    {
        StartCoroutine("FadeIn");
    }
}
