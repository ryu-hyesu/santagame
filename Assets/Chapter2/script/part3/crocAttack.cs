using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class crocAttack : MonoBehaviour
{
    public GameObject img;
    public GameObject bullet;
    public Transform pos;
    public float cooltime;
    public float currenttime;
    //crocPlayer crocPlayer;

    //public Image image;
    public bool start;
    public GameObject overView;
    private CanvasGroup cg;
    AudioSource audio;
    public float fadeTime = 1f; // 페이드 타임 
    float accumTime = 0f;
    public bool restart = false;

    public Image fadeImage; //시작 시 fade out

    // Start is called before the first frame update
    void Start()
    {
        start = false;
        audio = GetComponent<AudioSource>();
        cg = overView.GetComponent<CanvasGroup>();

        //시작 시 fade out
        if (fadeImage.color == new Color(0, 0, 0, 1))
        {
            StartCoroutine("FadeOut");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(img.activeSelf==false){
            if (GameObject.Find("Player").GetComponent<crocPlayer>().isDie == false)
            {
                start = true;
                if (currenttime <= 0)
                {
                    GameObject bulletcopy = Instantiate(bullet, pos.position, transform.rotation);
                    currenttime = cooltime;
                }
                currenttime -= Time.deltaTime;
            } 
            else
            {
                if (!restart){
                    StartCoroutine("reStart");
                    restart = true;
                }
            }
        }
    }

    IEnumerator reStart()
    {
        //yield return new WaitForSeconds(1.0f);

        audio.Play();

        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 1f;

        yield return new WaitForSeconds(1.0f);

        accumTime = 0f;

        SceneManager.LoadScene("croc");
    }

    IEnumerator FadeOut()
    {
        float f;
        for (f = 1f; f >= 0f; f -= 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            fadeImage.color = new Color(0, 0, 0, f);
        }
    }
}
