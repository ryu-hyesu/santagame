using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trunTrigger : MonoBehaviour
{
    public GameObject npc2;
    public GameObject hookTrigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(npc2.GetComponent<npcTrigger>().finished){
            hookTrigger.SetActive(true);
            Destroy(this);
        }
    }
}
