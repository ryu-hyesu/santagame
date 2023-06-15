using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMove : MonoBehaviour
{
    public float Speed;
    public float jumpPower;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    GameObject scanObject;

    float h;
    Vector2 dirVec;

    //부제
    Scene scene;
    public GameObject panelobject;
    private CanvasGroup cg;
    float fadeTime = 3f;
    bool titleFadeIn = false;

    // Start is called before the first frame update
    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        gameVariable.isTalk = false;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (scene.name == "1_livingroom")
        {
            cg = panelobject.GetComponent<CanvasGroup>();
            StartCoroutine("FadeIn");
            //StartCoroutine("canMove");
        }
    }

    void speicalMove()
    {
        if (Input.GetButtonDown("Jump") && !anim.GetBool("IsJumping") && !gameVariable.isTalk && !gameVariable.noMove && !ChatSystem2.GetInstance().dialogueIsPlaying)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("IsJumping", false);
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.1f, rigid.velocity.y);
        }

        if (Input.GetButton("Horizontal") && !gameVariable.isTalk && !ChatSystem2.GetInstance().dialogueIsPlaying)
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
    }

    void speicalMove2()
    {
        if (Input.GetButtonDown("Jump") && !anim.GetBool("IsJumping") && !gameVariable.isTalk && !gameVariable.noMove)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("IsJumping", false);
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.1f, rigid.velocity.y);
        }

        if (Input.GetButton("Horizontal") && !gameVariable.isTalk)
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
    }

    void animationMove()
    {
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("IsWalking", false);
        }
        else
        {
            anim.SetBool("IsWalking", true);
        }
    }

    void Update()
    {

        if (scene.name == "1_livingroom"){
            if (titleFadeIn)
            {
                speicalMove();
                animationMove();
            }
        }
        else if (scene.name == "2_hallway1F" || scene.name == "3_hallway2F" || scene.name == "7_hallway2F"
            || scene.name == "8_hallway1F" || scene.name == "10_hallway1F" || scene.name == "14_hallway1F" || scene.name == "15_hallway2F")
        {
            speicalMove2();
            animationMove();
        }
        
        else{
            speicalMove();
            animationMove();
        }

    }


    void checking()
    {
        if (rigid.velocity.y < 0)
        {

            Debug.DrawRay(rigid.position, Vector3.down*3, new Color(0, 1, 0));

            //Raycast(Ray 그려지기 시작하는 위치, Ray가 그려지는 방향, 길이, 정수 타입 레이어 필터링 넘버)
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 3, LayerMask.GetMask("floor"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 7.5f) //ray를 쏜 지점과 맞은 오브젝트간의 거리
                    anim.SetBool("IsJumping", false);
            }

        }

    }

    // Update is called once per frame
    void FixedUpdate() // 
    {
        if (scene.name == "1_livingroom")
        {
            if (titleFadeIn)
            {
                h = (gameVariable.isTalk || gameVariable.noMove) ? 0 : Input.GetAxisRaw("Horizontal");

                if (!gameVariable.noMove)
                    rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

                if (rigid.velocity.x > Speed)
                    rigid.velocity = new Vector2(Speed, rigid.velocity.y);
                else if (rigid.velocity.x < Speed * (-1))
                    rigid.velocity = new Vector2(Speed * (-1), rigid.velocity.y);


                checking();
            }
        }
        else
        {
            h = (gameVariable.isTalk || gameVariable.noMove) ? 0 : Input.GetAxisRaw("Horizontal");

            if (!gameVariable.noMove)
                rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            if (rigid.velocity.x > Speed)
                rigid.velocity = new Vector2(Speed, rigid.velocity.y);
            else if (rigid.velocity.x < Speed * (-1))
                rigid.velocity = new Vector2(Speed * (-1), rigid.velocity.y);


            checking();
        }
    
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(3.0f);
        float accumTime = 0f;
        while (accumTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 0f;
        StartCoroutine("canMove");
    }

    private IEnumerator canMove()
    {
        yield return new WaitForSeconds(1.0f);
        titleFadeIn = true;
    }
}
