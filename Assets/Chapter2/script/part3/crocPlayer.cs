using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crocPlayer : MonoBehaviour
{
    public float jumpPower;
    Rigidbody2D rigid; 
    bool isJumping;
    public bool isDie = false;

    //겜
    public GameObject attack;
    private bool isStart;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>(); 
    }


    void Update()
    {
        //Jump
        if (Input.GetButtonDown("Jump") && !isJumping && attack.GetComponent<crocAttack>().start)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
        }

    }
    private void FixedUpdate()
    {
        checking();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "fire")
        {
            isDie = true;
            Debug.Log("died");
        }
    }

    void checking()
    {
        //Landing Platform
        if (rigid.velocity.y < 0)
        {

            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            //rayCASTHIT RAY
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2, LayerMask.GetMask(new string[] { "floor" }));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.5f)
                    //Debug.Log(isJumping);
                    isJumping = false;
            }

        }

    }

}