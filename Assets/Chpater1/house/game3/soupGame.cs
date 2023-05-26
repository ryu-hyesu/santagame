using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class soupGame : MonoBehaviour
{
    public Image image;

    public GameObject slider;
    public GameObject soup;
    public Slider sliderImage;
    
    Animator anim;

    bool isStart = true;
    bool isEnd = false;

    public float minus;
    float time;

    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    int i;

    //È¿°úÀ½
    AudioSource audioSource;

    void Start()
    {
        if (image.color == new Color(0, 0, 0, 1))
        {
            StartCoroutine("FadeOut");
        }
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator end(){
        isEnd = false;
        yield return new WaitForSeconds(1);
        slider.SetActive(false);
    }
    public bool UpdateGame() {

        if(isStart){
            i = 0;
            spriteRenderer = soup.GetComponent<SpriteRenderer>();

            slider.SetActive(true);
            isStart = false;

            audioSource.Play();
        }

        if(isEnd){
            isEnd = false;
            slider.SetActive(false);

            audioSource.Stop();
            StartCoroutine("FadeIn");

            return true;
        }
        else{
            Debug.Log(isEnd);
            if(time < 3){
                time += Time.deltaTime*5;
                
            }
            else{
                if (sliderImage.value >= 0)
                    sliderImage.value -= minus;
                time = 0;
            }
        }  

        if(Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(sliderImage.value + ":" + sliderImage.maxValue);
            if((sliderImage.value == sliderImage.maxValue))
                isEnd = true;
            sliderImage.value += 3;

            if (i == 5) i = 0; else i++; 
            spriteRenderer.sprite=sprites[i];
        }
        return false;
        
    }

    IEnumerator FadeIn()
    {
        float f;
        for (f = 0f; f <= 1; f += 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0, 0, 0, f);
        }
        SceneManager.LoadScene("13_kitchen");
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
