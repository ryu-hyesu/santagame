using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class first : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private GameObject score;

    [Header("NPC position")]
    public Transform[] chatTr;
    
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool isStart;

    // Start is called before the first frame update
    void Start(){
        isStart = false;
        player.transform.position = playerPos.position;
    }
    void Update()
    {
        if(!isStart){
            isStart = true;
        StartCoroutine(gotoone1());
        }
    }

    private IEnumerator gotoone1(){
        gameVariable.isTalk = true;

        yield return new WaitForSeconds(4.0f);
        
        yield return new WaitUntil(()=> ChatSystem.GetInstance().upup(inkJSON, chatTr));

        gameVariable.isTalk = false;
        score.SetActive(true);
        Destroy(this);
    }

    // Update is called once per frame
    
}
