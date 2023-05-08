using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guideTrigger : MonoBehaviour
{
    bool closed;
    

    [Header("NPC position")]
    public GameObject npc;
    public Transform triggerPosition;
    public GameObject textBox;

    
    // Start is called before the first frame update
    void Start()
    {
        
        closed = false;
        textBox.transform.position = Camera.main.WorldToScreenPoint(triggerPosition.position + new Vector3(0,2,0));
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!npc.GetComponent<npcTrigger>().isDone)
            if(closed)
                textBox.SetActive(true);
            else
                textBox.SetActive(false);
        else
            textBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            closed = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            closed = false;
        }
    }
}
