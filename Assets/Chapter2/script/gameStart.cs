using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameStart : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject game;
    public GameObject img;
    bool isStart = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(img.activeSelf==false){
            if(Input.GetKeyDown(KeyCode.X) && !isStart){
                isStart= true;
                StartCoroutine(gotoone1());
            }
        }
    }

    private IEnumerator gotoone1(){
        yield return new WaitUntil(()=> game.GetComponent<sliderGame>().UpdateGame());
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("cake");
    }
}
