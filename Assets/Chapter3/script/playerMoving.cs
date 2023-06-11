using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMoving : MonoBehaviour
{
    //공통
    Scene scene;
    //public GameObject book;
    public GameObject guide;
    //bool isActive = false;

    public float Speed;
    public float jumpPower;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    //ch3_dialogue
    public NPCSentence3 npcSentence;
    public FadeInAndOut fadeScript;
    public bool isMoving = false;
    //bool finished;  //책장 넘어가는 애니메이션 실행하는 주석이었던 것 같음
    bool isDead = false;    //주인공 죽으면 isMoving=false 필요
    AudioSource audioSource;

    //street 에 있던 주석
   // public textmanger manger;
    //GameObject scanObject;

    int playerLayer, floorLayer;

    float h;
    Vector2 dirVec;

    //부제
    public GameObject panelobject;
    private CanvasGroup cg;
    float fadeTime = 3f;

    // 이중 점프 막는 bool 변수
    bool stillJumping;
    

    void Awake()
    {
        stillJumping = false;
        scene = SceneManager.GetActiveScene();
        rigid = GetComponent<Rigidbody2D>();
        anim =GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        floorLayer = LayerMask.NameToLayer("floor");

        isMoving = false;

        if(scene.name=="ch3_dialogue"){
            cg = panelobject.GetComponent<CanvasGroup>();
        }
    }

    void Update(){        
        if(scene.name == "ch3_dialogue"){
            //Debug.Log("isMoving, dialogueIsPlaying"+isMoving+" "+ChatSystem.GetInstance().dialogueIsPlaying);
            if(ChatSystem.GetInstance().dialogueIsPlaying){
                isMoving = false;
            }
            if(isMoving && !ChatSystem.GetInstance().dialogueIsPlaying){
                playerMove();
                animationMove();
            }else{
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsJumping", false);
            }
        }else if(scene.name == "ch3_game"){
            if(!isDead && !isMoving && guide.activeSelf == false){
                isMoving = true;
            }else if(isMoving){
                playerMove();
                animationMove();
                //Debug.Log("isJumping "+anim.GetBool("IsJumping"));
            }
        }else{
            playerMove();
            animationMove();
        }
    }

    void FixedUpdate()
    {
        float h = 0f;
        if(scene.name=="ch3_dialogue"){
            if(isMoving && !ChatSystem.GetInstance().dialogueIsPlaying){
                h = Input.GetAxisRaw("Horizontal");
                rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
            }
        }else if(scene.name=="ch3_game"){
            if(!isDead && isMoving && guide.activeSelf==false){
                h = Input.GetAxisRaw("Horizontal");
                rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
            }
        }else{
            h = (gameVariable.isTalk || gameVariable.noMove)? 0: Input.GetAxisRaw("Horizontal");
        
            if(!gameVariable.noMove)
                rigid.AddForce(Vector2.right*h, ForceMode2D.Impulse);

            if(rigid.velocity.x > Speed)
                rigid.velocity = new Vector2(Speed,rigid.velocity.y);
            else if(rigid.velocity.x < Speed*(-1))
                rigid.velocity = new Vector2(Speed*(-1),rigid.velocity.y);
        }
        /*
        if(scene.name=="ch3_game" && guide.activeSelf==false){
            playerSpeed();
            checkingFloor();
        }else if(scene.name != "ch3_game"){
            playerSpeed();
            checkingFloor();
        }
        */
        playerSpeed();
        checkingFloor();
    }
    //jump
    void playerMove()
    {
        if((scene.name=="ch3_game" && !isDead) || scene.name=="ch3_dialogue"){
            if(Input.GetButtonDown("Jump") && !anim.GetBool("IsJumping")){
                rigid.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
                anim.SetBool("IsJumping", true);
            }
        }else{
            if(Input.GetButtonDown("Jump") && !anim.GetBool("IsJumping") && !gameVariable.isTalk && !gameVariable.noMove && !stillJumping){
                stillJumping = true;
                rigid.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
                anim.SetBool("IsJumping",false);
            }
        }
        
        if(Input.GetButtonUp("Horizontal")){
            rigid.velocity = new Vector2(rigid.velocity.normalized.x*0.5f, rigid.velocity.y);
        }        
        if(Input.GetButton("Horizontal") && !gameVariable.isTalk)
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
    }
    //default
    void animationMove()
    {
        if(Mathf.Abs(rigid.velocity.x) <= 0.3){
            anim.SetBool("IsWalking",false);
        }
        else
            anim.SetBool("IsWalking",true);
    }

    void playerSpeed()
    {
        if(rigid.velocity.x > Speed)//Right max speed
            rigid.velocity = new Vector2(Speed, rigid.velocity.y);
        else if(rigid.velocity.x < Speed * (-1)) //left max speed
            rigid.velocity = new Vector2(Speed * (-1), rigid.velocity.y);
    }

    void checkingFloor(){
        if(rigid.velocity.y > 0){
            Physics2D.IgnoreLayerCollision(playerLayer, floorLayer, true);
        }else if(rigid.velocity.y < 0){
            Physics2D.IgnoreLayerCollision(playerLayer, floorLayer, false);
            
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0,1,0));
            //RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2, LayerMask.GetMask("floor"));
            RaycastHit2D rayHit = Physics2D.CircleCast(transform.position, 2.0f, Vector3.down, 0f,LayerMask.GetMask(new string[] {"floor"}));
            if(rayHit.collider != null){
                if(rayHit.distance < 0.1f)
                    anim.SetBool("IsJumping", false);
                    stillJumping = false;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Finish"){
            if(scene.name == "ch3_game")
                fadeScript.Fading();
        }
        if(other.tag == "Dead"){
            SceneManager.LoadScene("ch3_game");
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy"){
            //몬스터 밟기(attack)
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y){
                OnAttack(collision.transform);
            }
        }
    }
    void OnAttack(Transform enemy)
    {
        //Enemy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }
    public void OnDie()
    {   
        //player die
        audioSource.Play();
        isMoving = false;
        isDead = true;
        spriteRenderer.color = new Color(1,1,1,0.4f);
        spriteRenderer.flipY = true;
        GetComponent<BoxCollider2D>().enabled = false;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        Invoke("Reload", 2);
    }

    public void Reload()
    {
        Debug.Log("reload");
        SceneManager.LoadScene("ch3_game");
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

    public void FadingCG(){
        StartCoroutine("FadeIn");
    }

    private IEnumerator FadeIn(){
        yield return new WaitForSeconds(3.0f);
        float accumTime = 0f;
        while (accumTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 0f;
        isMoving = true;
    }
}