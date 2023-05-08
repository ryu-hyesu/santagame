using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class next : MonoBehaviour
{
    private bool playerInRange;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && !gameVariable.isTalk){
            if(Input.GetKeyDown(KeyCode.X)){
                StartCoroutine(FadeIn());
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

    IEnumerator FadeIn()
    {
        gameVariable.isTalk = true;
        float f;
        for(f = 0.05f;f<=1;f+=0.05f){
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0,0,0,f);
        }

        yield return null;

        SceneManager.LoadScene("EndShip");
        gameVariable.isTalk = false;
    }

    IEnumerator FadeOut()
    {
        float f;
        for(f = 1f;f>=0f;f-=0.05f){
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0,0,0,f);
        }
    } 
}
