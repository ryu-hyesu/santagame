using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCreditAnimManager : MonoBehaviour
{
    Animator anim;
    bool isEnded = false;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("credit") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !isEnded)
        {
            Debug.Log("엔딩크레딧 애니메이션 종료");
            isEnded = true;

            SceneManager.LoadScene("main_1");
        }
    }

}
