using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeskManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text stressCount;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private bool newDay;

    public void drinkBitch()
    {
        inventorySystem.chill();
    }

}
