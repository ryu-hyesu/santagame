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
        Text(200);
    }

    private void Start()
    {
        textIndex = 1;
        Story();
    }

    bool isStart = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isStart)
            {
                Story();
                isStart = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                Story();
            }
        }
    }

    void Text(int id)
    {
        if (textManager == null)
        {
            Debug.LogError("TextManager instance is null. Make sure it is assigned in the inspector.");
            return;
        }
        string textData = textManager.GetStory(id, textIndex);

        if (textData == null)
        {
            gm.moveScene("Book_start");
        }
        else
        {
            talktext = storyText.GetComponent<textEffect>();
            talktext.SetMsg(textData);
            textIndex++;
        }
    }
}
