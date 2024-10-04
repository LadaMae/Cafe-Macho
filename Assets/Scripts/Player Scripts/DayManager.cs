using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public int secondsElapsed = 0;
    public int minutesElapsed = 0;
    public int timeOfDay;
    public GameObject window;
    float counter = 0;
    // 0 = morning, 1 = afternoon, 2 = night;

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
}
