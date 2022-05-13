using UnityEngine;
public class A_Timer : MonoBehaviour
{

    double a_miliseconds;
    int a_seconds;
    int a_minutes;
    public static A_Timer a_timer;
    public bool a_countTime = false;
    public int a_ms;
    public int a_s;
    [HideInInspector] public string a_time;

    private void Awake()
    {
        a_timer = this;
    }

    private void FixedUpdate()
    {
        if (AudioManager.am.startTimerCB == true)
        {
            a_countTime = true;
        }

        else
        {
            a_countTime = false;
        }


        if (a_countTime)
        {
            a_miliseconds += 20;
            a_ms += 20;
            if (a_miliseconds > 999)
            {
                a_seconds += 1;
                a_s += 1;
                a_miliseconds = 0;
            }
            if (a_seconds > 59)
            {
                a_minutes += 1;
                a_seconds = 0;
            }
            string mil = a_miliseconds.ToString();
            mil = mil.PadLeft(2, '0');
            mil = mil.Remove(mil.Length - 1);
            string sec = a_seconds.ToString();
            string min = a_minutes.ToString();
            a_time = min.PadLeft(2, '0') + ":" + sec.PadLeft(2, '0') + ":" + mil;

            //Debug.Log("TIME IS " + Timer.timer.ms);
        }

    }
}
