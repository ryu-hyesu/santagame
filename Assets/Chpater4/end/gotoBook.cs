using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gotoBook : MonoBehaviour
{
    Animator anim;
    public GameObject trigger;

    private bool isDone;
    // Start is called before the first frame update
    void Start()
    {
        isDone = false;
        anim = GetComponent<Animator>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if(!isDone){
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("BookAnim") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f){
                isDone = true;
                StartCoroutine(sttart());
            }
        }
    }

    IEnumerator sttart(){
        yield return new WaitForSeconds(3f);

        trigger.SetActive(true);
        Debug.Log("dsfsd");

        yield return null;
    }



    
}
