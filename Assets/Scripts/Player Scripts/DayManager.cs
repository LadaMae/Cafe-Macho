using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    private int secondsElapsed = 0;
    private int minutesElapsed = 0;
    // 0 = morning, 1 = afternoon, 2 = night;
    private int timeOfDay;
    // NPCs can be talked to a max of 3 times per day
    [SerializeField] private int talkCounter = 0;
    [SerializeField] private GameObject window;
    [SerializeField] private DeskManager deskManager;
    

    private float counter = 0;
   

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= 1)
        {
            secondsElapsed++;
            counter = 0;
        }

        if (secondsElapsed >= 60)
        {
            minutesElapsed++;
            secondsElapsed = 0;
        }
        progressDay();
    }

    public void progressDay()
    {
        if (minutesElapsed < 4)
        {
            timeOfDay = 0;
        }
        else if (minutesElapsed >= 4 && minutesElapsed < 7)
        {
            timeOfDay = 1;
        }
        else
        {
            timeOfDay = 2;
        }

        changeWindow();

    }

    public void leave()
    {
        secondsElapsed = 0;
        minutesElapsed = 0;
        talkCounter = 0;
        deskManager.setNewDay();
    }

    public void changeWindow()
    {
        if (timeOfDay == 0)
        {
            window.GetComponent<SpriteRenderer>().material.color = Color.cyan;
        }
        else if (timeOfDay == 1)
        {
            window.GetComponent<SpriteRenderer>().material.color = Color.blue;
        }
        else if (timeOfDay == 2)
        {
            window.GetComponent<SpriteRenderer>().material.color = Color.gray;
        }
    }

    public int getTalkCounter()
    {
        return talkCounter;
    }

    public void incrementTalkCounter()
    {
        talkCounter++;
    }
}
