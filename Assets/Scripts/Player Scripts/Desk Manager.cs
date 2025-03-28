using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DeskManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private DialogueHandler dialogueHandler;
    [SerializeField] private DayManager dayManager;
    [SerializeField] private bool newDay;
    public NPCData chosenNPC;
    public NPCData[] talkableNPCS;
    public bool talkedToday = false;
    public UnityEngine.Vector3 startPos;


    void Start()
    {
        setNewDay();
    }

    public void onTalk()
    {
        if (dayManager.getTalkCounter() <= 3 && talkedToday == false)
        {

            // Start the coroutine for moving the NPC up, triggering dialogue, and moving them back down
            StartCoroutine(TalkSequence());
            talkedToday = true;
        }
    }

    // Coroutine to handle NPC moving up, triggering dialogue, and then moving down
    private IEnumerator TalkSequence()
    {

        // Move NPC up

        selectDialogue();

        // Wait for the dialogue to finish (I didnt wanna mess with dialogueHandler so i just made it a number)
        yield return new WaitForSeconds(5f);

        // Increment timesTalkedTo if there are more dialogues
        if (chosenNPC.timesTalkedTo < chosenNPC.YapperList.Length)
        {
            chosenNPC.timesTalkedTo++;
        }

        // Move NPC down coroutine
        //Commented in case we want to use it later
        yield return StartCoroutine(MoveNPCOut(chosenNPC, chosenNPC.moveDuration));

        dayManager.incrementTalkCounter();
    }

    //public void moveNPCToDeskC0()
    //{
    //    yield return StartCoroutine(MoveNPCToDesk(chosenNPC, chosenNPC.moveDuration));
    //}

    //public void moveNPCOutCO()
    //{
    //    yield return StartCoroutine(MoveNPCOut(chosenNPC, chosenNPC.moveDuration));
    //}

    

    public void setNewDay()
    {
        
        int randValue = Random.Range(0, talkableNPCS.Length);
        Debug.Log("Random Value: " + randValue);

        chosenNPC = talkableNPCS[randValue];

        for (int i = 0; i < talkableNPCS.Length; i++)
        {
            if (talkableNPCS[i].conversationAvailable == true)
            {
                chosenNPC = talkableNPCS[i];
            }
        }

        StartCoroutine(MoveNPCToDesk(chosenNPC, chosenNPC.moveDuration));

        talkedToday = false;

    }

    public void selectDialogue()
    {
        // Select reputation dialogue based on if the NPC is at the expected number of hearts and the conversation has not been done yet
        if (chosenNPC.hearts == 1 && chosenNPC.conversationAvailable == true)
        {
            dialogueHandler.TriggerDialogue(chosenNPC.OneHeart);
            chosenNPC.conversationAvailable = false;
        }
        else if (chosenNPC.hearts == 2 && chosenNPC.conversationAvailable == true)
        {
            dialogueHandler.TriggerDialogue(chosenNPC.TwoHeart);
            chosenNPC.conversationAvailable = false;
        }
        else if (chosenNPC.hearts == 3 && chosenNPC.conversationAvailable == true)
        {
            dialogueHandler.TriggerDialogue(chosenNPC.ThreeHeart);
            chosenNPC.conversationAvailable = false;
        }
        else // trigger standard dialogue
        {
            dialogueHandler.TriggerDialogue(chosenNPC.YapperList[chosenNPC.timesTalkedTo]);
        }

        chosenNPC.incrementReputation();
        Debug.Log("NPC reputation at " + chosenNPC.reputation);
    }

    public IEnumerator MoveNPCToDesk(NPCData npc, float duration)
    {
        startPos = npc.transform.position;
        UnityEngine.Vector3 animStartPos = new UnityEngine.Vector3(-11, 2, 0);
        UnityEngine.Vector3 targetPos = animStartPos + new UnityEngine.Vector3(3, 0, 0);

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            npc.transform.position = UnityEngine.Vector3.Lerp(animStartPos, targetPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        npc.transform.position = targetPos; // Ensure exact position at the end
    }

    // Coroutine to move the NPC out of the scene
    public IEnumerator MoveNPCOut(NPCData npc, float duration)
    {
        UnityEngine.Vector3 animStartPos = npc.transform.position;
        UnityEngine.Vector3 targetPos = animStartPos + new UnityEngine.Vector3(-3, 0, 0);
        UnityEngine.Vector3 origPos = startPos;

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            npc.transform.position = UnityEngine.Vector3.Lerp(animStartPos, targetPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        npc.transform.position = origPos; // Return NPC to original position at the end
    }
}
