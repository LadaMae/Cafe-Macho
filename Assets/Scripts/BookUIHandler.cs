using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookUIHandler : MonoBehaviour
{
    public UIHandler uIHandler;

    public void HideJournalUI()
    {
        uIHandler.HideJournalUI();
    }

    public void TurnUIPageRight()
    {
        uIHandler.turnPageRight();
    }

    public void TurnUIPageLeft()
    {
        uIHandler.turnPageLeft();
    }
}
