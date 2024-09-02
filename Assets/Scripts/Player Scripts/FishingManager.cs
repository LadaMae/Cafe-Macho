using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FishingManager : MonoBehaviour
{
    [SerializeField] private GameObject fishUI;
    [SerializeField] private Image fishImageUI;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private GameObject allFishView;
    [SerializeField] private Image fishImagePrefab;

    public List<Sprite> allFish = new List<Sprite>();
    private Dictionary<Sprite, bool> gottenFish = new Dictionary<Sprite, bool>();
    public Dictionary<Sprite, GameObject> collectedFishImages = new Dictionary<Sprite, GameObject>();

    void Start()
    {
        foreach (Sprite fsh in allFish)
        {
            GameObject newFish = Instantiate(fishImagePrefab.gameObject, allFishView.transform);
            newFish.GetComponent<Image>().sprite = fsh;
            collectedFishImages.Add(fsh, newFish);

            gottenFish.Add(fsh, false);
        }
    }
    public void generateFish()
    {
        gachaFish();
        fishUI.SetActive(true);
        inventorySystem.addFishCount();
    }

    public void gachaFish()
    {
        int rate = Random.Range(0, 100);

        if (rate == 99)
        {
            //rare fish
            fishImageUI.sprite = allFish[0];
            checkFish(allFish[0]);
            Debug.Log("rare");
        }
        else if (rate >= 94)
        {
            //uncommon fish
            Sprite fsh = allFish[Random.Range(1, 3)];
            fishImageUI.sprite = fsh;
            checkFish(fsh);
            Debug.Log("uncommon");
        }
        else
        {
            //common
            Sprite fsh = allFish[Random.Range(3, allFish.Count)];
            fishImageUI.sprite = fsh;
            checkFish(fsh);
            Debug.Log("common");
        }
    }

    public void checkFish(Sprite currentFish)
    {
        if(inventorySystem.fishCollection.ContainsKey(currentFish))
        {
            inventorySystem.fishCollection[currentFish] ++;
        }
        else
        {
            inventorySystem.fishCollection.Add(currentFish, 1);
        }
        
        if (gottenFish[currentFish] == false)
        {
            gottenFish[currentFish] = true;
            collectedFishImages[currentFish].GetComponent<Image>().color = Color.white;
        }
    }
}
