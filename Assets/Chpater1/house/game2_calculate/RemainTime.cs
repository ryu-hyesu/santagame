using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainTime : MonoBehaviour
{
    public Text text;
    public float rTime = 0f;

    void Update()
    {
        rTime -= Time.deltaTime;
        if(rTime < 0){
            rTime = 0;
        }
        text.text = "Remain Time : "+Mathf.Round(rTime);
    }
}
