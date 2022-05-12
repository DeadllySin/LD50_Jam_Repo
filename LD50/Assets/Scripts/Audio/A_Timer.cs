using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Timer : MonoBehaviour
{
    //public Text timerText;

    // a callback (like onClick for Buttons) for doing stuff when Countdown finished
    //public UnityEvent OnCountdownFinished;

    // here the countdown runs later
    private float timer;

    // for starting, pausing and stopping the countdown
    private bool runTimer;

    // just a control flag to avoid continue without pausing before
    private bool isPaused;

    float seconds;

    // start countdown with duration
    public void StartCountdown(float duration)
    {
        timer = duration;
        runTimer = true;
    }

    // stop and reset the countdown
    public void StopCountdown()
    {
        ResetCountdown();
    }

    private void Update()
    {
        if (!runTimer) return;

        if (timer <= 0)
        {
            Finished();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            runTimer = true;
            isPaused = false;
            timer = 0;
        }
            // reduces the timer value by the time passed since last frame
            timer -= Time.deltaTime;

        //string minutes = ((int)timer / 60).ToString();
        seconds = (timer % 60);

        Debug.Log(seconds);
        // a bit more readable
        //timerText.text = string.Format("{0}:{1}", minutes, seconds);
    }


    private void ResetCountdown()
    {
        //timerText.text = "0:00";
        runTimer = false;
        isPaused = false;
        timer = 0;
    }

    // called when the countdown exceeds and wasn't stopped before
    private void Finished()
    {
        // do what ever should happen when the countdown is finished

        // simpliest way is to call the UnityEvent and set up the rest via inspector
        // (same way as with onClick on Buttons)
        //OnCountdownFinished.Invoke();

        // and reset the countdown
        ResetCountdown();
    }
}
