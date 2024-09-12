using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
	[SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image speakerImage;
    [SerializeField] private TMP_Text speakerName;
    [SerializeField] private GameObject option1Button;
    [SerializeField] private TMP_Text option1text;
    [SerializeField] private GameObject option2Button;
    [SerializeField] private TMP_Text option2text;
    [SerializeField] private ServiceManager serviceManager;

    private OptionObject option1Object;
    private OptionObject option2Object;

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
            if (speakerImage != null)
            {
                speakerImage.gameObject.SetActive(true);
                speakerImage.sprite = currLine.speakerSprite;
            }
            else 
            {
                speakerImage.gameObject.SetActive(false);
            }


            //The following line of code makes it so that the for loop is paused until the user clicks the left mouse button.
            yield return new WaitUntil(()=>Input.GetMouseButtonDown(0));
            //The following line of code makes the coroutine wait for a frame so as the next WaitUntil is not skipped
            yield return null;
        }

        //shows options
        if (dialogueObject.optionObjects.Length > 0)
        {
            option1Button.SetActive(true);
            option1text.text = dialogueObject.optionObjects[0].option;
            option1Object = dialogueObject.optionObjects[0];
            if (dialogueObject.optionObjects.Length == 2)
            {
                option2Button.SetActive(true);
                option2text.text = dialogueObject.optionObjects[1].option;
                option2Object = dialogueObject.optionObjects[1];
            }

        }
        else
        {
            dialogueBox.SetActive(false);
            serviceManager.moveNPCDownCO();
        }
        
    }

    public void optionsButtonClick(int option)
    {
        option1Button.SetActive(false);
        option2Button.SetActive(false);
        if (option == 1)
        {
            if (option1Object.dialogueObjects == null)
            {
                dialogueBox.SetActive(false);
                serviceManager.moveNPCDownCO();
            }
            else
                StartCoroutine(MoveThroughDialogue(option1Object.dialogueObjects));
        }
            
        else if (option == 2)
        {
            if (option2Object.dialogueObjects == null)
            {
                dialogueBox.SetActive(false);
                serviceManager.moveNPCDownCO();
            }
                
            else
                StartCoroutine(MoveThroughDialogue(option2Object.dialogueObjects));
        }
            
    }
}

