using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class putting : MonoBehaviour
{
    public GameObject moveTrigger;
    private NumberSystem TheNumber;
    // Start is called before the first frame update
    void Start()
    {
        TheNumber = FindObjectOfType<NumberSystem>();
    }


    private bool isStart = false;

    // Update is called once per frame
    void Update()
    {
        
        if(flag && !isStart){
            if(Input.GetKeyDown(KeyCode.X)){
                Debug.Log("와");
            isStart = true;
            StartCoroutine(Acoroutine());
            }
        }

        
    }

    public bool flag;
    public int CorrectNumber;

    IEnumerator Acoroutine(){
        gameVariable.isTalk = true;
        TheNumber.ShowNumber(CorrectNumber);
        yield return new WaitUntil(()=> !TheNumber.activated);

        isStart = false;
        gameVariable.isTalk = false;

        if(TheNumber.GetResult()){
            moveTrigger.SetActive(true);
            Destroy(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            flag = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            flag = false;
        }
    }
}
