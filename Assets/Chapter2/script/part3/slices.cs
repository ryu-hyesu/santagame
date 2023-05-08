using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class slices : MonoBehaviour
{
    Vector3 RightPosition;
    public bool InRightPosition;
    public bool selected;
    public Transform mainCamera;
    AudioSource correct;
    

    void Start()
    {
        RightPosition = transform.position;
        transform.position = mainCamera.position + new Vector3(Random.Range(-8f, -1f), Random.Range(-4f, 4f),1);
        correct = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        if(Vector3.Distance(transform.position, RightPosition) < 0.4f){
            if(!selected){
                if(!InRightPosition){
                    puzzleAnswer.answer++;
                    correct.Play();
                    transform.position = RightPosition;
                    InRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                }
            }
        }
    }
}
