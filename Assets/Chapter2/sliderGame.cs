using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderGame : MonoBehaviour
{

    public Slider slider;
    public int speed;

    //초록색 바의 값을 알아야 함
    public float minPos;
    public float maxPos;
    public RectTransform pass;
    public int satNum;
    public bool flag;

    public GameObject canvas;
    bool isEnd=false;
    public bool isStart = true;
    public GameObject cake;
    Animator anim;

    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    AudioSource audioSoure;

    private void Start() {
        audioSoure = GetComponent<AudioSource>();
    }
    
    public void SetAtk(){
        slider.value = 0;
        minPos = pass.anchoredPosition.x;
        maxPos = pass.sizeDelta.x + minPos;

        
        StartCoroutine(ComboAtk());
    }

    public IEnumerator ComboAtk(){
        yield return null;
        while (!(Input.GetKeyDown(KeyCode.X))){
            if(slider.value==slider.maxValue)
                flag = false;
            if(slider.value==slider.minValue)
                flag = true;
            
            if(flag)
                slider.value += Time.deltaTime*speed;
            else
                slider.value -= Time.deltaTime*speed;
            yield return null;
        }

        Debug.Log(slider.value);
        audioSoure.Play();

        if(slider.value>= minPos && slider.value <= maxPos){
            satNum++;

            yield return new WaitForSeconds(1);

            if(satNum == 1){
                spriteRenderer.sprite=sprites[1];
                //anim.Play("mix");
                SetAtk();
            }
            else{
                satNum = 0;
                isEnd= true;
                canvas.SetActive(false);
                spriteRenderer.sprite=sprites[2];
                //anim.Play("end");
            }
        }
        else
        {
            spriteRenderer.sprite=sprites[0];
            //anim.Play("start");
            isAtk = false;
            satNum = 0;
        }
        slider.value = 0;
    }    

    bool isAtk=false;
    public bool UpdateGame() {
        if(isStart){
            spriteRenderer = cake.GetComponent<SpriteRenderer>();
            canvas.SetActive(true);
            isStart= false;
            anim = cake.GetComponent<Animator>();
        }

        if(isEnd){
            return true;
        }

        if(Input.GetKeyDown(KeyCode.X) && !isAtk){
            
            isEnd = false;
            isAtk = true;
            SetAtk();
        }

        return false;
    }

    
}
