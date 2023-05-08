
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcTrigger : MonoBehaviour
{
    [Header("NPC position")]
    public Transform[] chatTr;
    
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public bool playerInRange;
    public bool isDone;
    public bool isPlaying;
    //end
    public bool finished = false;
    AudioSource audioSoure;
    //npc FlipX
    public GameObject npcing;
    SpriteRenderer npcFlipx;
    public GameObject player;
    SpriteRenderer playerFlipx;
    // Start is called before the first frame update
    void Start()
    {
        audioSoure = GetComponent<AudioSource>();
        playerInRange = false;
        if (npcing != null) {
            npcFlipx = npcing.GetComponent<SpriteRenderer>();
            playerFlipx = player.GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && !gameVariable.isTalk){
            if(Input.GetKeyDown(KeyCode.X)){
                isDone = true;
                StartCoroutine(gotoone1());
            }
        }
    }
    
    bool first = true;
    private IEnumerator gotoone1(){
        gameVariable.isTalk = true;

        if (npcing != null) 
            npcFlipx.flipX = playerFlipx.flipX == npcFlipx.flipX ? true : false;

        yield return new WaitForSeconds(0.5f);
        audioSoure.Play();
        yield return new WaitUntil(()=> ChatSystem.GetInstance().upup(inkJSON, chatTr));
        finished = true;
        
        if (npcing != null) 
            npcFlipx.flipX = false;

        gameVariable.isGame = true;
        gameVariable.isTalk = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            playerInRange = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            playerInRange = false;
        }
    }

}
