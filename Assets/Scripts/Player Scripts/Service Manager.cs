using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class ServiceManager : MonoBehaviour
{
    [SerializeField] private GameObject serviceUI;
    [SerializeField] private TMP_Text pastryCount;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private DeskManager deskManager;

    public void showServiceUI()
    {
        serviceUI.SetActive(true);
        pastryCount.text = "Pastries: " + inventorySystem.fishCount;
        
    }

    public void sellFsh()
    {
        inventorySystem.sellFsh();
    }

}
