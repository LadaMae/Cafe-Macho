using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class ServiceManager : MonoBehaviour
{
    [SerializeField] private GameObject serviceUI;
    [SerializeField] private TMP_Text pastryCount;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private DeskManager deskManager;
    [SerializeField] private DayManager dayManager;
    public DialogueHandler dialogueHandler;
    public NPCData chosenNPC;
    public NPCData[] talkableNPCS;
    public bool inUse = false;


    public void showServiceUI()
    {
        if (inUse != true)
        {
            serviceUI.SetActive(true);
            inUse = true;
        }
        
    }

    public void setInUseOff()
    {
        inUse = false;
    }

     public void onTalk()
    {
        if (dayManager.getTalkCounter() <= 3)
        {
            int randValue = Random.Range(0, talkableNPCS.Length);
            Debug.Log("Random Value: " + randValue);

            chosenNPC = talkableNPCS[randValue];

            // Start the coroutine for moving the NPC up, triggering dialogue, and moving them back down
            StartCoroutine(TalkSequence());
        }
        else
        {
            inUse = false;
        }
    }

    // Coroutine to handle NPC moving up, triggering dialogue, and then moving down
    private IEnumerator TalkSequence()
    {

            // Move NPC up
            yield return StartCoroutine(MoveNPCUp(chosenNPC, chosenNPC.moveDist, chosenNPC.moveDuration));

            // Trigger dialogue
            dialogueHandler.TriggerDialogue(chosenNPC.YapperList[chosenNPC.timesTalkedTo]);

            // Wait for the dialogue to finish (I didnt wanna mess with dialogueHandler so i just made it a number)
            yield return new WaitForSeconds(5f);

            // Increment timesTalkedTo if there are more dialogues
            if (chosenNPC.timesTalkedTo < chosenNPC.YapperList.Length)
            {
                chosenNPC.timesTalkedTo++;
            }

            // Move NPC down coroutine
            //Commented in case we want to use it later
            //yield return StartCoroutine(MoveNPCDown(chosenNPC, chosenNPC.moveDist, chosenNPC.moveDuration));

        dayManager.incrementTalkCounter();
        inUse = false;
        chosenNPC.incrementReputation();
        Debug.Log("NPC reputation at " + chosenNPC.reputation);
    }

    public void moveNPCDownCO()
    {
        StartCoroutine(MoveNPCDown(chosenNPC, chosenNPC.moveDist, chosenNPC.moveDuration));
    }

    // Coroutine to move the NPC up
    public IEnumerator MoveNPCUp(NPCData npc, float distance, float duration)
    {
        UnityEngine.Vector3 startPos = npc.transform.position;
        UnityEngine.Vector3 targetPos = startPos + new UnityEngine.Vector3(0, distance, 0);

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            npc.transform.position = UnityEngine.Vector3.Lerp(startPos, targetPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        npc.transform.position = targetPos; // Ensure exact position at the end
    }

    // Coroutine to move the NPC down
    public IEnumerator MoveNPCDown(NPCData npc, float distance, float duration)
    {
        UnityEngine.Vector3 startPos = npc.transform.position;
        UnityEngine.Vector3 targetPos = startPos - new UnityEngine.Vector3(0, distance, 0); // Subtract to move down

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            npc.transform.position = UnityEngine.Vector3.Lerp(startPos, targetPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        npc.transform.position = targetPos; // Ensure exact position at the end
    }
}