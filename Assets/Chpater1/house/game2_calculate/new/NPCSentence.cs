using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSentence : MonoBehaviour
{
    public GameObject remainTimeTextUI;

    public TextAsset inkJSON;
    public Transform[] chatTr;
    private bool playerInRange;

    public string[] questions;
    public bool did = false;

    AudioSource audioSoure;

    private void Start()
    {
        audioSoure = GetComponent<AudioSource>();
        playerInRange = false;
    }
    public void Update()
    {
        if(playerInRange && !ChatSystem2.GetInstance().dialogueIsPlaying){
            if(!did && Input.GetKeyDown(KeyCode.X)){
                remainTimeTextUI.GetComponent<RemainTime>().rTime = 5f;
                ChatSystem2.GetInstance().Ondialogue(inkJSON, chatTr);
                audioSoure.Play();
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
