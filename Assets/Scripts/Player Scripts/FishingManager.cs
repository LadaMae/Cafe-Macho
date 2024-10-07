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

    [SerializeField] private GameObject IndicatorUI;
    [SerializeField] private GameObject ProgressBarUI;
    [SerializeField] private GameObject fishingText;

    public List<Sprite> allFish = new List<Sprite>();
    private Dictionary<Sprite, bool> gottenFish = new Dictionary<Sprite, bool>();
    public Dictionary<Sprite, GameObject> collectedFishImages = new Dictionary<Sprite, GameObject>();

    //Fishing Minigame
    public Image indicator; 
    public List<GameObject> targetZonePrefabs;
    public int numberOfTargetZones = 3;
    private float minAngle = 0f; 
    private float maxAngle = 360f;
    public float rotationSpeed = 200f;
    private bool isFishing = false; //CHANGE TO FALSE WHEN DONE TESTING
    private float rotationAngle = 0f;
    private List<TargetZone> targetZones = new List<TargetZone>();

    //Progression Bar
    [SerializeField] private Slider progressBar;
    [SerializeField] private int maxPoints = 100; //Win con to get a fsh.
    public int currPoints = 0;


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

    void showFishingUI()
    {
        IndicatorUI.SetActive(true);
        ProgressBarUI.SetActive(true);
        fishingText.SetActive(true);
    }

    void hideFishingUI()
    {
        IndicatorUI.SetActive(false);
        ProgressBarUI.SetActive(false);
        fishingText.SetActive(false);
    }
    public void PlayMinigame()
    {
        //Make sure PBar is reset.
        progressBar.maxValue = maxPoints;
        progressBar.value = 0;

        isFishing = true;

        showFishingUI();

        GenerateTargetZones();
    }

    
    void DestroyTargetZones()
    {
        //Ensure clear board.
        foreach(var zone in targetZones)
        {
            Destroy(zone.gameObject);
        }
        targetZones.Clear();
    }
    
    void GenerateTargetZones()
    {
        
        DestroyTargetZones();

        //Generate the new zones
        List<float> usedAngles = new List<float>();
        for (int i = 0; i < numberOfTargetZones; i++)
        {
            float randomAngle = GenerateAngleWithoutOverlap(usedAngles);
            GameObject targetZonePrefab = targetZonePrefabs[UnityEngine.Random.Range(0, targetZonePrefabs.Count)];
            CreateTargetZone(randomAngle, targetZonePrefab);
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

     void CreateTargetZone(float angle, GameObject targetZonePrefab)
    {
        //Instantiate new zone
        GameObject newTargetZone = Instantiate(targetZonePrefab, indicator.transform.parent);
        TargetZone targetZoneScript = newTargetZone.GetComponent<TargetZone>();

        if (targetZoneScript == null)
        {
            Debug.LogError("TargetZone script is missing on prefab: " + targetZonePrefab.name);
            return;
        }

        RectTransform targetZoneRect = newTargetZone.GetComponent<RectTransform>();

        //I hate math.
        float radians = angle * Mathf.Deg2Rad;
        float radius = indicator.rectTransform.rect.width / 2f; //Completely based on the indicator being a circle

        float xPos = Mathf.Cos(radians) * radius;
        float yPos = Mathf.Sin(radians) * radius;
        
        targetZoneRect.anchoredPosition = new Vector2(xPos,yPos);

        //Correct angle and place it in perimiter of indicator object.
        targetZoneRect.localRotation = Quaternion.Euler(0,0, angle);
        targetZones.Add(targetZoneScript);
    }

    public void generateFish()
    {
        GenerateTargetZones();
        
        gachaFish();
        fishUI.SetActive(true);
        inventorySystem.addTotalFishCount();
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
        //checks if current fish obtained before
        if(inventorySystem.fishCollection.ContainsKey(currentFish))
        {
            //adds if current fish obtained
            inventorySystem.addToIndFishCount(currentFish);
        }
        else
        {
            //if not, adds fish to fishcollection
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
        bool allGoodZoneTriggered = true;
        bool regenIsAGo = false;

        for (int i = 0; i < targetZones.Count; i++)
        {
            var targetZone = targetZones[i];
            RectTransform targetZoneRect = targetZone.GetComponent<RectTransform>();

            //30f is arbitrary to set the size of the target zone.
            float targetStart = targetZoneRect.eulerAngles.z - 20f;
            float targetEnd = targetZoneRect.eulerAngles.z + 20f;
            
            if(angle >= targetStart && angle <= targetEnd)
            {
                
                if(!targetZone.isTriggered)
                {
                    AddPoints(targetZone.points);
                    targetZone.isTriggered = true;
                    
                    if(targetZone.points > 0)
                    {
                        Debug.Log("Good Zone!" + targetZone.points);
                        targetZone.zoneSprite.color = Color.green; //For feedback
                    }
                    else
                    {
                        Debug.Log("Bad Zone!  " + targetZone.points);
                        targetZone.zoneSprite.color = Color.red; //For feedback
                    }
                }
            }
              //Check if we need to reset.
            if(targetZone.points > 0 && !targetZone.isTriggered)
            {
                allGoodZoneTriggered = false;
            }
        }

            if(allGoodZoneTriggered && progressBar.value < progressBar.maxValue && isFishing)
            {
                Debug.Log("All good zones triggered, we are switching!!!!");
                regenIsAGo = true;
            }

            if(regenIsAGo)
            {
                // WHY ARE YOU WORKING AHHHHHHH
                StartCoroutine(GenerateTargetZonesNextFrame());
            }

        //We might want to repeat the rotation at nauseum until player gets the fish as to avoid stress?
        Debug.Log("Lost Fish");
    }

    IEnumerator GenerateTargetZonesNextFrame()
    {
        yield return null;
        GenerateTargetZones();
    }

    void AddPoints(int points)
    {
        currPoints += points;
        progressBar.value = currPoints;

        if(currPoints >= maxPoints)
        {
            hideFishingUI();
            ResetProgress();
            Debug.Log("Caught Fsh");
            generateFish();
            isFishing = false;
            DestroyTargetZones();
        }

    }

    void ResetProgress()
    {
        currPoints = 0;
        progressBar.value = currPoints;
    }

}
