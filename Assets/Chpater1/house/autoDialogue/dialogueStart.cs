using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueStart : MonoBehaviour
{
    public TextAsset inkJSON;
    public Transform[] chatTr;
    bool dialogueEnd;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "dialogue" && dialogueEnd == false)
        {
            Debug.Log("��� �ڵ�");
            ChatSystem2.GetInstance().Ondialogue(inkJSON, chatTr);
            dialogueEnd = true;
        }
    }
}
