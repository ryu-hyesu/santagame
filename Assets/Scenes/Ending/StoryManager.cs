using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public gameManager gm;
    public TextManager textManager;
    public int textIndex;

    textEffect talktext;

    public void Story()
    {
        StoryData storyData = GetComponent<StoryData>();
        Text(storyData.id);
        //storyText.text = "";
    }

    bool isStart = false;

    void Update()
    {
        if(!isStart){
            Story();
            isStart = true;
        }
        else
            if (Input.GetKeyDown(KeyCode.Space))
                Story();
        
        
    }

    void Text(int id)
    {
        string textData = textManager.GetStory(id, textIndex);

        if (textData == null)
            gm.moveScene("street");

        talktext = storyText.GetComponent<textEffect>();
        talktext.SetMsg(textData);
        textIndex++;
    }

}
