using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueSystem/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    public DialogueLine[] dialogueLines;
    public OptionObject[] optionObjects;
}
