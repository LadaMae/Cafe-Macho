using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishObject
{
    public Sprite thisFish;
    public int numOfFish;
    public List<FishUnlockDialogue> fishUnlockDialogues;

    public void addFishCount()
    {
        numOfFish++;
    }

    public void setFishCount(int num)
    {
        numOfFish = num;
    }
    
    public string checkAllJournalEntries(int num)
    {
        string result = "";
        foreach(FishUnlockDialogue fishUnlockDialogue in fishUnlockDialogues)
        {
            if (num >= fishUnlockDialogue.unlockNum)
            {
                result += fishUnlockDialogue.unlockedDialogue + "\n";
            }
        }
        return result;
    }
}

[System.Serializable]
public struct FishUnlockDialogue
{
    public int unlockNum;
    public string unlockedDialogue;
}
