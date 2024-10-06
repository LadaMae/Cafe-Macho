using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public int fishCount = 0;
    //number of each fish type
    public Dictionary<Sprite, int> fishCollection = new Dictionary<Sprite, int>();
    [SerializeField] private FishingManager fishingManager;
    public bool newDay = true;

    public struct FishObject
    {
        public int numOfFish {get; set;}
    }

    public void addFishCount()
    {
        fishCount++;
    }

}
