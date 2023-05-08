using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blockGame : MonoBehaviour
{
    public Text talkText;
    public Animator talkPanel;
    public bool isAction;
    public bool b1;
    public bool b2;
    public bool b3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "block1" && !b1)
        {
            talkPanel.SetBool("isShow",  true);
            talkText.text = "해적 대사1";
            //Debug.Log("해적 대사1");
            b1 = true;
            b2 = false;
            b3 = false;
        }

        if (col.gameObject.tag == "block2" && !b2)
        {
            talkPanel.SetBool("isShow", true);
            talkText.text = "해적 대사2";
            //Debug.Log("해적 대사2");
            b2 = true;
            b1 = false;
            b3 = false;
        }

        if (col.gameObject.tag == "block3" && !b3)
        {
            talkPanel.SetBool("isShow", true);
            talkText.text = "해적 대사3";
            //Debug.Log("해적 대사3");
            b3 = true;
            b1 = false;
            b2 = false;
        }

        if (col.gameObject.tag == "obj" && (b1 || b2 || b3))
        {
            talkPanel.SetBool("isShow", false);
            //Debug.Log("대사x"); 
            b1 = false;
            b2 = false;
            b3 = false;
        }
    }
}
