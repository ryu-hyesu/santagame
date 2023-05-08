using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ch1GameGuide : MonoBehaviour
{
    public GameObject img;
    public GameObject text;

    void Start()
    {
        img.SetActive(false);
        text.SetActive(false);
        Invoke("OnImg", 0);
        Invoke("OnText", 1);
        Invoke("OffImg", 4);
    }

    public void OnImg()
    {
        img.SetActive(true);
    }

    public void OnText()
    {
        text.SetActive(true);
    }

    public void OffImg()
    {
        img.SetActive(false);
        text.SetActive(false);
    }
}
