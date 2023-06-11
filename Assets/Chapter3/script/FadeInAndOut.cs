using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInAndOut : MonoBehaviour
{
    public Image img;
    public GameObject black;

    public playerMoving playerMoving;

    Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();

        if(scene.name == "ch3_dialogue" || scene.name=="ch3_game")
            Fading2();
    }

    public void Fading(){
        black.SetActive(true);
        StartCoroutine("FadeIn");
    }

    public void Fading2(){
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeIn()
    {
        float f=0;
        for(f = 0.05f;f<=1;f+=0.05f){
            yield return new WaitForSeconds(0.05f);
            img.color = new Color(0,0,0,f);
        }
        if(f >= 1){
            yield return new WaitForSeconds(0.05f);
            if(scene.name == "ch3_game")
                SceneManager.LoadScene("ch3_dialogue");
            //StartCoroutine("FadeOut");
        }
    }
    
    IEnumerator FadeOut()
    {
        float f=1;
        img.color = new Color(0,0,0,f);
        
        for(;f>=0;f-=0.05f){
            yield return new WaitForSeconds(0.05f);
            img.color = new Color(0,0,0,f);
        }
        if(f <= 0){
            yield return new WaitForSeconds(0.05f);
            playerMoving.FadingCG();
        }
    }
}