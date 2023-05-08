using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startTrigger : MonoBehaviour
{
    public GameObject game;
    bool startGame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) && !startGame){
            
            startGame= true;
            //StartCoroutine(startgame());
        }
    }
/*
    IEnumerator startgame(){
        yield return new WaitUntil(()=>game.GetComponent<puzzleGame>().gaming());
        Destroy(this);
    }*/
}
