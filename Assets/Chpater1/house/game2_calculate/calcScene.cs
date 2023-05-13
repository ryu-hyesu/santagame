using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calcScene : MonoBehaviour
{
    public ChatSystem2 chatSystem2;
    public GameObject nextScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (chatSystem2.gameSceneNum == 3)
        {
            nextScene.SetActive(true);
        }    
    }
}
