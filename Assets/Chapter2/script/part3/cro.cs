using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class cro : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;

    private float maxHp = 100;
    private float curHp = 100;

    public GameObject croc;
    public GameObject slider;
    public GameObject img;
    public GameObject corcAttack;

    public Image fadeImage; //성공 시 fade in

    void Start()
    {
        hpbar.value = (float)curHp / (float)maxHp;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && img.activeSelf==false && !corcAttack.GetComponent<crocAttack>().restart)
        {
            if (curHp > 10)
            {
                curHp -= 10;
            }
            else if (curHp == 10)
            {
                //curHp = 0;
                Destroy(croc);
                Destroy(slider);

                StartCoroutine("FadeIn");
                //SceneManager.LoadScene("SHIP");
                Debug.Log("끝");
            }
        }

        HandleHp();
    }

    private void HandleHp()
    {
        hpbar.value = Mathf.Lerp(hpbar.value, (float)curHp / (float)maxHp, Time.deltaTime * 10);
    }

    IEnumerator FadeIn()
    {
        float f;
        for (f = 0f; f <= 1; f += 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            fadeImage.color = new Color(0, 0, 0, f);
        }
        SceneManager.LoadScene("SHIP");
    }
}
