using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameVariable : MonoBehaviour
{
    public static bool isTalk = false;
    public static bool noMove = false;
    public static bool isGame = false;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        isGame = false;
        noMove = false;
        isTalk = false;
    }

    
}
