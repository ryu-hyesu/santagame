using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerManger : MonoBehaviour
{
    
    public float Speed;
    public float jumpPower;

    
   // public textmanger manger;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    GameObject scanObject;

    float h;
    Vector2 dirVec;

    
   // GameObject scanObject;
    bool stillJumping;
    // Start is called before the first frame update
    void Awake()
    {
        stillJumping = false;
        rigid = GetComponent<Rigidbody2D>();
        anim =GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void speicalMove(){
        
        if(Input.GetButtonDown("Jump") && !anim.GetBool("IsJumping") && !gameVariable.isTalk && !gameVariable.noMove && !stillJumping){
            rigid.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
            anim.SetBool("IsJumping",false);
            stillJumping = true;
        }

        if(Input.GetButtonUp("Horizontal")){
             // 벡터의 크기를 1로 만든 상태
            rigid.velocity = new Vector2(rigid.velocity.normalized.x*0.1f, rigid.velocity.y);
        }

        if(Input.GetButton("Horizontal") && !gameVariable.isTalk){
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
    }

    void animationMove(){
        //횡 이동 단위 값 == 0
        if(Mathf.Abs(rigid.velocity.x) < 0.3){
            anim.SetBool("IsWalking",false);
        }
        else
            anim.SetBool("IsWalking",true);
    }

    void Update(){
            speicalMove();
            animationMove();
    }


    void checking(){
        //Landing Platform (빛이 시작하는 위치,밑에서 쏨,컬러)
        //점프할때
        if(rigid.velocity.y <0){
            
            Debug.DrawRay(rigid.position,Vector3.down,new Color(0,1,0));
        
                    //rayCASTHIT RAY에 닿은 오브젝트
            RaycastHit2D rayHit = Physics2D.CircleCast(transform.position, 2.0f, Vector3.down, 0f,LayerMask.GetMask(new string[] {"floor","npc"}));

            if(rayHit.collider != null){
                if(rayHit.distance <0.5f){
                    stillJumping = false;
                    anim.SetBool("IsJumping",false);
                }
            }
        
        }
        
    }

    // Update is called once per frame
    void FixedUpdate() // 
    {
        
        //manger.isAction ? 0:
        h = (gameVariable.isTalk || gameVariable.noMove)? 0: Input.GetAxisRaw("Horizontal");
        
        if(!gameVariable.noMove)
            rigid.AddForce(Vector2.right*h, ForceMode2D.Impulse);

        if(rigid.velocity.x > Speed)
            rigid.velocity = new Vector2(Speed,rigid.velocity.y);
        else if(rigid.velocity.x < Speed*(-1))
            rigid.velocity = new Vector2(Speed*(-1),rigid.velocity.y);


        checking();
        
        
    }



}
