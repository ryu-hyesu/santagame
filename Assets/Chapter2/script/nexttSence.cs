using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class nexttSence : MonoBehaviour
{
    private bool playerInRange;
  
    private bool checkbool = false;
    private bool isStart = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange){
        if(Input.GetKeyDown(KeyCode.X) ){
            Debug.Log("sdfs");
            SceneManager.LoadScene("cake");  
        }
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Debug.Log("DDD");
            playerInRange = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            playerInRange = false;
        }
    }
}
