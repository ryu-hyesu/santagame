using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoving : MonoBehaviour
{
    public float Speed=10;
    public float jumpPower=10;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
    }
    void FixedUpdate()
    {
        float h = 0f;
        h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        playerSpeed();
        checkingFloor();
    }

    void playerMove()
    {
        if(Input.GetButtonDown("Jump")){
            rigid.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
        }
        
        if(Input.GetButtonUp("Horizontal")){
            rigid.velocity = new Vector2(rigid.velocity.normalized.x*0.5f, rigid.velocity.y);
        }        
        if(Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
    }

    void playerSpeed()
    {
        if(rigid.velocity.x > Speed)//Right max speed
            rigid.velocity = new Vector2(Speed, rigid.velocity.y);
        else if(rigid.velocity.x < Speed * (-1)) //left max speed
            rigid.velocity = new Vector2(Speed * (-1), rigid.velocity.y);
    }


    void checkingFloor(){
        if(rigid.velocity.y < 0){
            
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0,1,0));
            //RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2, LayerMask.GetMask("floor"));
            RaycastHit2D rayHit = Physics2D.CircleCast(transform.position, 2.0f, Vector3.down, 0f,LayerMask.GetMask(new string[] {"floor"}));
            
        }
    }
}
