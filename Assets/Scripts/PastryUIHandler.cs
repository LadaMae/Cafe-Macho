using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastryUIHandler : MonoBehaviour
{
    public GameObject pastryUI;

    public void hidePastryUI()
    {
        pastryUI.SetActive(false);
    }

}
