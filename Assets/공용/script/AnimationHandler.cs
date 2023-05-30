using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationHandler : MonoBehaviour
{
    #region Fields
    #endregion Fields
 
    #region Members
    private Animator m_Animator;
 
    #endregion Members
 
 
    #region Methods

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }
 
    public void EnterNextScene()
    {
        // 애니메이션 재생
        //m_Animator.Play("Animation_Name");
    }
 
    public void OnEnterNextScene()
    {
        // 애니메이션이 끝난 후 처리
        if(SceneManager.GetActiveScene().name == "SHIP")
            SceneManager.LoadScene("ch3_game");
        else if (SceneManager.GetActiveScene().name == "16_children")   //ch1 -> ch2
            SceneManager.LoadScene("New Scene");
        else
            SceneManager.LoadScene("ch3_game");
    }
 
    #endregion Methods
}
