using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class clickButton : MonoBehaviour
{

    public GameObject button;
    private bool isClicked;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        isClicked = false;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !isClicked)
        {

            isClicked = true;
            StartCoroutine(sttart());


        }
    }

    IEnumerator sttart(){
        

        

        float f;
        for(f = 0.05f;f<=1;f+=0.05f){
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0,0,0,f);
        }

        yield return null;

        SceneManager.LoadScene("Book_start");
    }

    public void clickNext(){
        SceneManager.LoadScene("Book_start");
    }
}
