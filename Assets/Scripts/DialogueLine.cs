using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    public Sprite speakerSprite;
    [TextArea] public string dialogueText;
}
