using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appear : MonoBehaviour
{
    public GameObject preObject;
    public GameObject posObject; 
    public GameObject questionMark;
    private bool posTrigger;
    // Start is called before the first frame update
    void Start()
    {
        posTrigger = posObject.GetComponent<npcTrigger>();

    }

    // Update is called once per frame
    void Update()
    {
        if (preObject.GetComponent<npcTrigger>().finished == true)
            questionMark.SetActive(true);
        
        if (posObject.activeSelf == true)
            questionMark.SetActive(false);
    }
}
