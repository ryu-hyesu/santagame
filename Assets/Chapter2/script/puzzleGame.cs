using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class puzzleGame : MonoBehaviour
{
    public GameObject success;
    public GameObject guide;
    public GameObject selectedPiece;
    public int OrderInLayer = 1;
    public bool finishGame=false;
    bool startGame= false;


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
}


    
