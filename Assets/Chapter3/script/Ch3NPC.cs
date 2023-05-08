using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ch3NPC : MonoBehaviour
{
    Scene scene;
    public ChatSystem3 chat3;
    public TextAsset[] inkJSON;
    //public GameObject enemy;
    //EnemyMove enemyMove;
    public Transform[] chatTr;
    public bool playerInRange;

    public bool did = false;
    bool flag = false;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
        //enemyMove = enemy.GetComponent<EnemyMove>();
    }

    private void Start()
    {
        playerInRange = false;
    }

    public void Update()
    {
        if(!did && playerInRange){
            did = true;
            chat3.Ondialogue(inkJSON[0], chatTr);
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
