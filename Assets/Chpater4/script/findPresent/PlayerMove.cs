using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    public float Speed;
    public float jumpPower;
    
    Transform npc;
    Transform item;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    BoxCollider2D colliders;
    //public ChatSystem chatSystem;

    public bool isMoving = true;
    public int itemCount=0;
    public CountItem countItem;

    public Camera maincamera;
    [SerializeField]
    private GameObject score;
    public GameObject children;
    public Transform posing;

    float h;
    AudioSource audio;
     private BoxCollider2D boxCollider;
 public LayerMask layerMask;

    void Awake()
    {
       
        rigid = GetComponent<Rigidbody2D>();
        anim =GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }

    void speicalMove(){
        
        if(Input.GetButtonDown("Jump") && !anim.GetBool("IsJumping") && !gameVariable.isTalk && !gameVariable.noMove){
            rigid.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
            anim.SetBool("IsJumping",true);
        }

        if(Input.GetButtonUp("Horizontal")){
             // 벡터의 크기를 1로 만든 상태
            rigid.velocity = new Vector2(rigid.velocity.normalized.x*0.1f, rigid.velocity.y);
        }

        if(Input.GetButton("Horizontal") ){
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
                if(rayHit.distance <0.5f)
                    anim.SetBool("IsJumping",false);
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

    public bool isEnd = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collider");
        if(other.tag == "Item"){
            itemCount++;
            audio.Play();
            other.gameObject.SetActive(false);
            countItem.GetItem(itemCount);
        }
        else if(other.tag == "boom"){
            itemCount = 0;
            for(int j = 0; j <10; j++)
                children.transform.GetChild(j).gameObject.SetActive(true);
            countItem.GetItem(itemCount);
            gameVariable.isTalk = true;
            StartCoroutine(gotoon());
        }
        else if(other.tag == "finish"){
            Debug.Log("finish");
            if(itemCount == 8){
                maincamera.GetComponent<cameraController>().mapSize.x = 11.79f;
                maincamera.GetComponent<cameraController>().center.x= 1.3f;
                Speed = 2;
                jumpPower = 10;
                isEnd = true;
                score.SetActive(false);
                //SceneManager.LoadScene("Example1_"+(manager.stage+1));
                Debug.Log("finish");
            }
        }
        else if(other.tag == "hide"){
            Debug.Log("hide");
            if(Input.GetKeyDown(KeyCode.X)){
                Debug.Log("finish");
                other.gameObject.SetActive(false);
            }
        }

    }

    public Image image;

    private IEnumerator gotoon(){

        gameVariable.isTalk = true;
        float f;
        for(f = 0.01f;f<=1;f+=0.05f){
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0,0,0,f);
        }
        transform.position = posing.transform.position;
        yield return null;

        for(f = 1f;f>=0f;f-=0.05f){
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0,0,0,f);
        }
        yield return new WaitForSeconds(1);


        
        yield return null;
        gameVariable.isTalk = false;
    }
}
