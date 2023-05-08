using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextSence : MonoBehaviour
{

    // PLAYER가 TRIGGER에 들어왔는지 여부
    private bool playerInRange;
    [SerializeField]
    GameObject FinshNPC2;

    
    

    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && FinshNPC2.GetComponent<npcTrigger>().finished){
            if(Input.GetKeyDown(KeyCode.X)){
                SceneManager.LoadScene("SHIP");
                
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
