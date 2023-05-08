using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class letterTrigger : MonoBehaviour
{
    public GameObject letter;
    public GameObject camerashake;
    CinemachineImpulseSource impulse;

    bool istalking;
    bool isOpen;

    [Header("NPC position")]
    public Transform[] chatTr;
    
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public GameObject DiaglogueText;
    int i = 0;
    bool isPlayerInside;
    void Start(){
        impulse = camerashake.transform.GetComponent<CinemachineImpulseSource>();
        isOpen = true;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) && isPlayerInside){

            if (isOpen){
                gameVariable.isTalk = true;
                letter.SetActive(true);
                isOpen = false; 
            }
            else {
                letter.SetActive(false);

                if(DiaglogueText.activeSelf){
                    StartCoroutine(gotoone1());
                    DiaglogueText.SetActive(false);
                }
                else{
                    gameVariable.isTalk = false;
                    isOpen = true;
                }
            }
       
        }
    }

    private IEnumerator gotoone1(){
        gameVariable.isTalk = true;

        impulse.GenerateImpulse(10f);
        
        yield return new WaitForSeconds(2f);
        
        yield return new WaitUntil(()=> ChatSystem.GetInstance().upup(inkJSON, chatTr));

        yield return null;
        gameVariable.isGame = true;
        gameVariable.isTalk = false;
        isOpen = true;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            isPlayerInside = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            isPlayerInside = false;
        }
    }

    public void choices(){
        transform.GetComponent<MeshRenderer>().sortingOrder += 1;   
    }
}
