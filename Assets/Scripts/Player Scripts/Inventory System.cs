using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public int fishCount = 0;
    //number of each fish type
    public Dictionary<Sprite, int> fishCollection = new Dictionary<Sprite, int>();
    public FishObject[] journalEntries;
    public Dictionary<Sprite, FishObject> FishJournalEntries = new Dictionary<Sprite, FishObject>();
    [SerializeField] private FishingManager fishingManager;
    [SerializeField] private UIHandler uiHandler;
    public bool newDay = true;

    //Kermit the farm here
    void Start()
    {
        foreach (FishObject fish in journalEntries)
        {
            FishJournalEntries.Add(fish.thisFish, fish);
            fishCollection.Add(fish.thisFish, 0);
        }
        Debug.Log(FishJournalEntries.Count);
    }

    public void addTotalFishCount()
    {
        fishCount++;
    }

    public void addToIndFishCount(Sprite currentFish)
    {
        fishCollection[currentFish]++;
        uiHandler.updateJournal();
    }

    //gets the text of a given fish
    public string getText(Sprite currentFish)
    {
        return FishJournalEntries[currentFish].checkAllJournalEntries(fishCollection[currentFish]);
    }

}
