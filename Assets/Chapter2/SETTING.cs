using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SETTING : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameVariable.isGame);
        Debug.Log(gameVariable.noMove);
        Debug.Log(gameVariable.isTalk);
        gameVariable.isTalk = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
