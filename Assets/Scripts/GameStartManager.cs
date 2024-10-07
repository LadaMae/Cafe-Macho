using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{

    public DialogueObject dimitryObject;
    public DialogueHandler dialogueHandler;
    public GameObject tutorialText;
    // Start is called before the first frame update
    void Start()
    {
        dialogueHandler = FindAnyObjectByType<DialogueHandler>();
        tutorialText.SetActive(false);
        StartScene();
    }

    public void StartScene()
    {
        dialogueHandler.TriggerDialogue(dimitryObject);
        tutorialText.SetActive(true);
    }
}
