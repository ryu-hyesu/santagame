using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipMove : MonoBehaviour
{
    public float moveSpeed;
    float xx;
    float h;
    //public Animator anim;
    Rigidbody2D rigid;
    public float Speed;
    void Start(){
        //gameVariable.noMove = true;
        rigid = GetComponent<Rigidbody2D>();
    }
    // private void Update() {
    //     if (h == 0)
    //         anim.SetBool("goMove",false);
    //     else
    //         anim.SetBool("goMove",true);
    // }


    private void FixedUpdate() {

        float keyHorizontal = gameVariable.isTalk ? 0 : Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Speed * Time.smoothDeltaTime * keyHorizontal , Space.World);
    }

    void playerSpeed()
    {
        if(rigid.velocity.x > Speed)//Right max speed
            rigid.velocity = new Vector2(Speed, rigid.velocity.y);
        else if(rigid.velocity.x < Speed * (-1)) //left max speed
            rigid.velocity = new Vector2(Speed * (-1), rigid.velocity.y);
    }



}
