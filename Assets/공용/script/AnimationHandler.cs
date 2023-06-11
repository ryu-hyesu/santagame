using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimationHandler : MonoBehaviour
{
    #region Fields
    #endregion Fields
 
    #region Members
    private Animator m_Animator;
 
    #endregion Members
 
 
    #region Methods

    public getPaper gp;
    public GameObject fade;
    Image fadeImg;

    public bool did = false;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        fadeImg = fade.GetComponent<Image>();
    }
 
    public void EnterNextScene()
    {
        // 애니메이션 재생
        //m_Animator.Play("Animation_Name");
    }
 
    public void OnEnterNextScene()
    {
        // 애니메이션이 끝난 후 처리
        //fade효과 후 씬 이동
        //1. fade효과
        if(fade.activeSelf==false)
            fade.SetActive(true);
         StartCoroutine(gp.FadeIn(fadeImg, 1));
        //씬이동 - getPaper에서 처리
    }


 
    #endregion Methods
}
