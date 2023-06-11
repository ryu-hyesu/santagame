using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D collider;

    public bool die = false;

    //ch3_game : 몬스터 죽을 때 효과음
    public AudioClip audioAttack;
    AudioSource audioSource;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioAttack;
        
        Think();
        Invoke("Think", 2);
    }

    //1초에 50~60회 돈다
    void FixedUpdate()
    {
        //Debug.Log("die"+die);
        
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //platform check
        Vector2 frontVec = new Vector2(rigid.position.x+nextMove*0.2f, rigid.position.y);
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("floor"));
        if(rayHit.collider == null){
            Turn();
        }
    }
    //행동지표를 바꿔줄 함수 생성
    void Think()
    {
        //최대값은 랜덤값에 포함이 안 되고 int를 사용하므로 최대값을 2로 지정
        nextMove = Random.Range(-1,2);

        anim.SetInteger("WalkSpeed", nextMove);
        if(nextMove != 0)
            spriteRenderer.flipX = nextMove==1;

        
        //생각하는 시간이 랜덤으로 바뀜
        float nextThinkTime = Random.Range(1.5f, 3.5f);
        //재귀 이용(계속해서 도므로 딜레이 필요)
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove==1;

        CancelInvoke();
        Invoke("Think", 2);
    }
    public void OnDamaged()
    {
        audioSource.Play();
        //Sprite alpha
        spriteRenderer.color = new Color(1,1,1,0.4f);
        //sprite flip y
        spriteRenderer.flipY = true;
        //collider disable
        collider.enabled = false;
        //die effect jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //destroy
        die = true;
        Invoke("DeActive", 5);
    }
    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
