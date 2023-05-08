using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class gameTrigger : MonoBehaviour
{


    private bool playerInRange;

    public GameObject npc;
    public GameObject npc2;
    public remain remain;


    // Start is called before the first frame update
    void Start()
    {
        npc.SetActive(remain.abool);
        npc2.SetActive(!remain.abool);
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && gameVariable.isGame && !remain.isTRY){
            if(Input.GetKeyDown(KeyCode.X)){
                npc.SetActive(remain.abool);
                npc2.SetActive(!remain.abool);
                remain.abool = false;
                remain.isTRY = true;
                SceneManager.LoadScene("cakeGame");
                
            }
        }
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
