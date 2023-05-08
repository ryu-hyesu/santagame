using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();        
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("end") &&
        anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f){
            Destroy(gameObject);
        }

   
    }

    
}
