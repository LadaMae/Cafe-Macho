using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public int fishCount = 0;

    public Dictionary<Sprite, int> fishCollection = new Dictionary<Sprite, int>();
    [SerializeField] private FishingManager fishingManager;
    [SerializeField] private TMP_Text pastryCountTxt;
    [SerializeField] private TMP_Text moneeCountTxt;
    [SerializeField] private TMP_Text stressCountTxt;
    public double monee = 0;
    public double moneeIncrement = 1;
    public bool newDay = true;


    public void addFishCount()
    {
        fishCount++;
        pastryCountTxt.text = "Pastries: " + fishCount;
    }

    public void sellFsh()
    {
        List<Sprite> allFish = fishingManager.allFish;
        if (fishCollection.ContainsKey(allFish[0]))
        {
            monee += ((fishCollection[allFish[0]] * 10) * moneeIncrement);
        }
        if (fishCollection.ContainsKey(allFish[1]))
        {
            monee += ((fishCollection[allFish[1]] * 5)  * moneeIncrement);
        }
        if (fishCollection.ContainsKey(allFish[2]))
        {
            monee += ((fishCollection[allFish[2]] * 5)  * moneeIncrement);
        }
        for (int i = 3; i < allFish.Count; i++)
        {
            if (fishCollection.ContainsKey(allFish[i]))
            {
                monee += (fishCollection[allFish[i]] * moneeIncrement);
            }
        }
        fishCount = 0;
        fishCollection.Clear();
        pastryCountTxt.text = "Pastries: " + fishCount;
        moneeCountTxt.text = "Monee: " + monee;
        newDay = true;
        gameObject.GetComponent<PlayerMovement>().moveSpeed = 5;
    }

    public void updateMoneeTxt()
    {
        moneeCountTxt.text = "Monee: " + monee;
    }

}
