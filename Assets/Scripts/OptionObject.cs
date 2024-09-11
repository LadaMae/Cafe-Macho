using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DialogueSystem/OptionObject")]
public class OptionObject : ScriptableObject
{
    public string option;
    public DialogueObject dialogueObjects;
}
