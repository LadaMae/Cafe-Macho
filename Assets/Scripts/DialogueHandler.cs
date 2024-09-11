using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
	[SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image speakerImage;
    [SerializeField] private TMP_Text speakerName;
	// public DialogueObject currentDialogue;
    public bool isAuto;
    public float autoTime;
    public bool textSpeed;

    public void TriggerDialogue(DialogueObject dialogueObject)
    {
        StartCoroutine(MoveThroughDialogue(dialogueObject));
    }
    
    private IEnumerator MoveThroughDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        for(int i = 0; i < dialogueObject.dialogueLines.Length; i++)
        {
            DialogueLine currLine = dialogueObject.dialogueLines[i];
            dialogueText.text = currLine.dialogueText;
            speakerName.text = currLine.speakerName;
            speakerImage.sprite = currLine.speakerSprite;

            //The following line of code makes it so that the for loop is paused until the user clicks the left mouse button.
            yield return new WaitUntil(()=>Input.GetMouseButtonDown(0));
            //The following line of code makes the coroutine wait for a frame so as the next WaitUntil is not skipped
            yield return null;
        }
        dialogueBox.SetActive(false);
    }
}

