using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class textEffect : MonoBehaviour
{
    Scene scene;
    public int CharPerSenconds;
    string targetMsg;
    //TextMeshProUGUI msgText;
    TextMeshProUGUI msgText;
    int index;

    public bool isAnimation;
    // Start is called before the first frame update
    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
        msgText = GetComponent<TextMeshProUGUI>();
    }

    public void SetMsg(string msg)
    {
        if (isAnimation)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            msg = msg.Replace("\\n", "\n");
            targetMsg = msg;
            EffectStart();

        }

    }

    void EffectStart()
    {
        isAnimation = true;
        msgText.text = "";
        index = 0;

        Invoke("Effecting", 1.0f / CharPerSenconds);
    }

    void Effecting()
    {

        if (msgText.text == targetMsg)
        {
            EffectEnd();
            Debug.Log("완");
            // ChatSystem.GetInstance().ShowChoices();
            return;
        }

        msgText.text += targetMsg[index];
        index++;

        Invoke("Effecting", 1.0f / CharPerSenconds);
    }

    void EffectEnd()
    {
        if (scene.name == "ch3_dialogue")
        {
            ChatSystem.GetInstance().activateChoices = true;
            ChatSystem.GetInstance().madeChoices = false;
        }
        if (scene.name == "9_livingroom")
        {
            ChatSystem2.GetInstance().activateChoices = true;
            ChatSystem2.GetInstance().madeChoices = false;
        }
        if (scene.name == "street" || scene.name == "New Scene" || scene.name == "cake" || scene.name == "SHIP" || scene.name == "EndShip")
        ChatSystem.GetInstance().ShowChoices();
        isAnimation = false;
    }

}