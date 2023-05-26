using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class dialogue
{
    public int npcIndex;
    [TextArea(3,10)]
    public string sentences;
}
