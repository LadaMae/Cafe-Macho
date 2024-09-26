using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    //Fishing Minigame
    public Image indicator; 
    public GameObject targetZonePrefab;
    public int numberOfTargetZones = 3;
    public float minAngle = 0f; //Change to private later, keeping public for testing purposes.
    public float maxAngle = 360f;
    public float rotationSpeed = 200f;
    private bool isFishing = true;
    private float rotationAngle = 0f;
    private List<Image> targetZones = new List<Image>(); 
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

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.R)){
            GenerateTargetZones();
        }
        
        if(isFishing)
        {
            rotationAngle += rotationSpeed * Time.deltaTime;
            indicator.rectTransform.rotation = Quaternion.Euler(0,0, rotationAngle);

            if(Input.GetKeyDown(KeyCode.Space))
            {
               CheckIfSucessful();
            }
        }
    }

    void GenerateTargetZones()
    {
        //Ensure clear board.
        foreach(var zone in targetZones)
        {
            Destroy(zone.gameObject);
        }
        targetZones.Clear();

        //Generate the new zones
        List<float> usedAngles = new List<float>();
        for (int i = 0; i < numberOfTargetZones; i++)
        {
            float randomAngle = GenerateAngleWithoutOverlap(usedAngles);
            CreateTargetZone(randomAngle);
            usedAngles.Add(randomAngle);
        }


    }

    float GenerateAngleWithoutOverlap(List<float> usedAngles)
    {
        float randomAngle = UnityEngine.Random.Range(minAngle, maxAngle);
        float minAngleDiff = 30f; //can change and shouldn't break.

        bool isOverlapping;
        int maxAttempts = 30;
        int attempts = 0;

        do //do's so cool 
        {
            isOverlapping = false;
            foreach (float usedAngle in usedAngles)
            {
                if(Mathf.Abs(Mathf.DeltaAngle(randomAngle, usedAngle)) < minAngleDiff)
                {
                    isOverlapping = true;
                    break;
                }
            }

            if(isOverlapping)
            {
                randomAngle = UnityEngine.Random.Range(minAngle, maxAngle);
            }

            attempts++;
            if(attempts > maxAttempts)
            {
                Debug.Log("Max Attempts Reached to generate fishing target zones.");
                break;
            }
        } while(isOverlapping);

        return randomAngle;
    }

     void CreateTargetZone(float angle)
    {
        //Instantiate new zone
        GameObject newTargetZone = Instantiate(targetZonePrefab, indicator.transform.parent);
        Image targetZoneImage = newTargetZone.GetComponent<Image>();

        //I hate math.
        float radians = angle * Mathf.Deg2Rad;
        float radius = indicator.rectTransform.rect.width / 2f; //Completely based on the indicator being a circle

        float xPos = Mathf.Cos(radians) * radius;
        float yPos = Mathf.Sin(radians) * radius;
        
        targetZoneImage.rectTransform.anchoredPosition = new Vector2(xPos,yPos);

        //Correct angle and place it in perimiter of indicator object.
        targetZoneImage.rectTransform.localRotation = Quaternion.Euler(0,0, angle);
        targetZones.Add(targetZoneImage);
    }

    public void generateFish()
    {
        gachaFish();
        fishUI.SetActive(true);
        inventorySystem.addFishCount();
    }

    public void gachaFish()
    {
        int rate = UnityEngine.Random.Range(0, 100);

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
            Sprite fsh = allFish[UnityEngine.Random.Range(1, 3)];
            fishImageUI.sprite = fsh;
            checkFish(fsh);
            Debug.Log("uncommon");
        }
        else
        {
            //common
            Sprite fsh = allFish[UnityEngine.Random.Range(3, allFish.Count)];
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

    void CheckIfSucessful()
    {
        float angle = indicator.rectTransform.eulerAngles.z; //If Img type rectTransform is missing in between the .

        foreach (var targetZone in targetZones)
        {
            //30f is arbitrary to set the size of the target zone.
            float targetStart = targetZone.rectTransform.eulerAngles.z - 30f;
            float targetEnd = targetZone.rectTransform.eulerAngles.z + 30f;

            if(angle >= targetStart && angle <= targetEnd)
            {
                Debug.Log("Successful Catch!");
                targetZone.color = Color.green; //For feedback
                return;
            }
        }

        //We might want to repeat the rotation at nauseum until player gets the fish as to avoid stress?
        Debug.Log("Lost Fish");
        //isFishing = false;
    }

}
