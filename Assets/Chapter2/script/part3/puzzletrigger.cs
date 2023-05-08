using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class puzzletrigger : MonoBehaviour
{
    private bool playerInRange;
    public GameObject npc1;
    remain remain;

    //두번 실행되지 않게 하는 것
    private bool isDone = false;

    // Start is called before the first frame update
    void Start()
    {
        
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && npc1.GetComponent<npcTrigger>().finished && !isDone){
            if(Input.GetKeyDown(KeyCode.X)){
                isDone = true;
                ShipVariable.abool = false;
                SceneManager.LoadScene("puzzle");
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
