using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    
    public new string name;
    public int timesTalkedTo;
    public int reputation;
    public int hearts;
    public DialogueObject[] YapperList;
    public DialogueObject OneHeart;
    public DialogueObject TwoHeart;
    public DialogueObject ThreeHeart;
    public bool conversationAvailable = false;

    public float moveDist = 2f;
    public float moveDuration = 1f; 

    public void incrementReputation()
    {
        if (conversationAvailable == false)
        {
            reputation++;
            if (reputation == 1)
            {
                hearts = 1;
                conversationAvailable = true;
            }
            else if (reputation == 3)
            {
                hearts = 2;
                conversationAvailable = true;
            }
            else if (reputation == 6)
            {
                hearts = 3;
                conversationAvailable = true;
            }
        }
        
    }

    public int getHearts()
    {
        return hearts;
    }
}
