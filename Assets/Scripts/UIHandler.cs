using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private FishingManager fishingManager;
    [SerializeField] private InventorySystem inventorySystem;
    public List<Sprite> allFish;
    public Image fishJournalImage;
    public TMP_Text fishJournalText;
    public Animator JournalAnimator;
    public Animator TabAnimator;
    public GameObject JournalUI;
    private int currentFishIndex = 0;

    void Start()
    {
        allFish = fishingManager.allFish;
    }

    public void updateJournal()
    {
        fishJournalImage.sprite = allFish[currentFishIndex];
        fishJournalText.text = inventorySystem.getText(allFish[currentFishIndex]);
    }
    public void closeBook()
    {
        JournalAnimator.SetTrigger("CloseBook");
        TabAnimator.SetTrigger("CloseTabs");
    }

    public void HideJournalUI()
    {
        JournalUI.SetActive(false);
        currentFishIndex = 0;

    }

    public void turnPageRight()
    {
        currentFishIndex++;
        if (currentFishIndex >= allFish.Count)
        {
            currentFishIndex = 0;
        }
        fishJournalImage.sprite = allFish[currentFishIndex];
        fishJournalText.text = inventorySystem.getText(allFish[currentFishIndex]);
    }

    public void turnPageLeft()
    {
        currentFishIndex--;
        if (currentFishIndex < 0)
        {
            currentFishIndex = allFish.Count - 1;
        }
        fishJournalImage.sprite = allFish[currentFishIndex];
        fishJournalText.text = inventorySystem.getText(allFish[currentFishIndex]);
    }
}
