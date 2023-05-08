using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSence : MonoBehaviour
{
    private bool playerInRange;
    
    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && !gameVariable.isTalk){
            if(Input.GetKeyDown(KeyCode.X)){
                SceneManager.LoadScene("findPresent");
                
            }
        }
    }
    
    bool first = true;
    

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
