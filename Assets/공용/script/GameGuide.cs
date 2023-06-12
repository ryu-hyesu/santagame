using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGuide : MonoBehaviour
{
    public GameObject img;
    public GameObject text;

    void Start()
    {
        Invoke("OffImg", 3);
    }

    public void OffImg()
    {
        img.SetActive(false);
        text.SetActive(false);
    }
}
