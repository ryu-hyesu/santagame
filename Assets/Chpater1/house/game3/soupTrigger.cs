using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soupTrigger : MonoBehaviour
{
    /*
    [Header("NPC position")]
    public Transform[] chatTr;
    
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;*/

    private bool playerInRange;
    public bool isDone;
    public bool isPlaying;
    public GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
        StartCoroutine(gotoone());
    }

    // Update is called once per frame
    /*void Update()
    {
        if(playerInRange && !isPlaying){
            if(Input.GetKeyDown(KeyCode.X)){
                isDone = true;
                StartCoroutine(gotoone());
            }
        }
    }*/

    private IEnumerator gotoone(){
        isPlaying = true;
        yield return new WaitForSeconds(4.0f);
        if (game){
            yield return new WaitUntil(()=> game.GetComponent<soupGame>().UpdateGame());
        
        }
        isPlaying = false;
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
