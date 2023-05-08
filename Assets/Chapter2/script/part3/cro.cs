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
                SceneManager.LoadScene("SHIP");
                Debug.Log("끝");
            }
        }

        HandleHp();
    }

    private void HandleHp()
    {
        hpbar.value = Mathf.Lerp(hpbar.value, (float)curHp / (float)maxHp, Time.deltaTime * 10);
    }
}
