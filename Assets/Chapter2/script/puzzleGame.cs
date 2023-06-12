using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class puzzleGame : MonoBehaviour
{
    public GameObject success;
    public GameObject guide;
    public GameObject selectedPiece;
    public int OrderInLayer = 1;
    public bool finishGame=false;
    bool startGame= false;

    public Image fadeImage; //fade in,out

    void Start()
    {

        //Ω√¿€ Ω√ fade out
        if (fadeImage.color == new Color(0, 0, 0, 1))
        {
            StartCoroutine("FadeOut");
        }
    }

    private void Update() 
    {
        if(guide.activeSelf==false){
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if(hit.collider != null){
                    if(hit.transform.CompareTag("Puzzle")){
                        if(!hit.transform.GetComponent<slices>().InRightPosition){
                            selectedPiece = hit.transform.gameObject;
                            selectedPiece.GetComponent<slices>().selected = true;
                            selectedPiece.GetComponent<SortingGroup>().sortingOrder = OrderInLayer;
                            OrderInLayer++;
                        }
                    }
                }
            }

            if(Input.GetMouseButtonUp(0)){
                if(selectedPiece!= null)
                    selectedPiece.GetComponent<slices>().selected = false;
                selectedPiece = null;
            }

            if(selectedPiece != null){
                Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                selectedPiece.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0); 
            }
            if(puzzleAnswer.answer > 13){
                finishGame = true;
                success.SetActive(true);
                Invoke("NextScene",1);
            }
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene("SHIP");
    }

    /*IEnumerator FadeIn()
    {
        float f;
        for (f = 0f; f <= 1; f += 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            fadeImage.color = new Color(0, 0, 0, f);
        }
        SceneManager.LoadScene("SHIP");
    }*/

    IEnumerator FadeOut()
    {
        float f;
        for (f = 1f; f >= 0f; f -= 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            fadeImage.color = new Color(0, 0, 0, f);
        }
    }
}
