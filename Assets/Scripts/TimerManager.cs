using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private GameObject value;
    private int minutes;
    private int seconds;
    private float timer; //Delta time aggregator
    private int secondsPassed = 0;
    private bool timerDone;

    // Start is called before the first frame update
    void Start()
    {
        value = GetComponentInParent<Transform>().Find("Value").gameObject;
        timerDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!timerDone)
        {
            timer += Time.deltaTime;
            //Debug.Log("Timer running. Time on timer: " + timer);
            if (Mathf.Floor(timer) > secondsPassed)
            {
                Debug.Log("Timer math occuring.");
                if (seconds == 0)
                {
                    if (minutes == 0)
                    {
                        timerDone = true;
                    }
                    else
                    {
                        minutes--;
                        seconds = 60; //Will be decremented in 
                    }
                }
                secondsPassed++;
                seconds--;
                updateTimer();
            }
        }
    }
    /// <summary>
    /// Returns in seconds how much time is left on the timer.
    /// </summary>
    /// <returns></returns>
    public int getTime()
    {
        return seconds + (minutes * 60);
    }
    /// <summary>
    /// Sets the time, in seconds.
    /// </summary>
    /// <param name="time">The time for the timer to run. Ex: 2 minutes would pass in 120.</param>
    public void setTimer(int time)
    {
        timerDone = false;
        timer = 0.0f;
        secondsPassed = 0;
        if(time > 59)
        {
            minutes = Mathf.FloorToInt((float)time / (float)60);
        }
        else
        {
            minutes = 0;
        }
        seconds = time - (60 * minutes);
        updateTimer();
    }
    /// <summary>
    /// Returns whether the timer is done running or not.
    /// </summary>
    /// <returns>True if done, false if not.</returns>
    public bool getDone()
    {
        return timerDone;
    }

    private void updateTimer()
    {
        if(seconds > 9)
        {
            value.GetComponent<UnityEngine.UI.Text>().text = minutes + ":" + seconds;
        }
        else
        {
            value.GetComponent<UnityEngine.UI.Text>().text = minutes + ":0" + seconds;
        }
    }
}
