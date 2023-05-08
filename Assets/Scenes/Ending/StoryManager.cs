using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public TextMeshProUGUI storyText;

    public TextManager textManager;
    public int textIndex;

    textEffect talktext;

    public void Story()
    {
        StoryData storyData = GetComponent<StoryData>();
        Text(storyData.id);
        //storyText.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Story();
        }
    }

    void Text(int id)
    {
        string textData = textManager.GetStory(id, textIndex);

        if (textData == null)
            return;

        talktext = storyText.GetComponent<textEffect>();
        talktext.SetMsg(textData);
        textIndex++;
    }

}
