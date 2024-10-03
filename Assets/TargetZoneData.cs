using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class TargetZone : MonoBehaviour
{
    public int points;
    public UnityEngine.UI.Image zoneSprite;

    public bool isTriggered = false;

    void Start()
    {
        if(zoneSprite == null)
        {
            zoneSprite = GetComponent<UnityEngine.UI.Image>();

            if(zoneSprite ==null)
            {
                Debug.LogError("No Image component found on " + gameObject.name);
            }
        }
    }
}
