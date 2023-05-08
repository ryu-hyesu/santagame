using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class endScript : MonoBehaviour
{
    [Header("NPC position")]
    public Transform[] chatTr;
    
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;
    public bool isDone;
    public bool isPlaying;
    public Image image;

    public GameObject memo;
    
    //end
    public bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDone){
            
            isDone = true;
            StartCoroutine(gotoone1());
            
        }
    }
    
    bool first = true;
    private IEnumerator gotoone1(){
        gameVariable.isTalk = true;

        float f;
        for(f = 1f;f>=0f;f-=0.05f){
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0,0,0,f);
        }

        yield return null;

        
        yield return new WaitUntil(()=> ChatSystem.GetInstance().upup(inkJSON, chatTr));
        finished = true;
        memo.SetActive(true);

        gameVariable.isGame = true;
        gameVariable.isTalk = false;

        
        
        //SceneManager.LoadScene("Book_start");
    }

    
}
