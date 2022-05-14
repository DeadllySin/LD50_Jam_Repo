using UnityEngine;
public class Timer : MonoBehaviour
{
    double miliseconds;
    int seconds;
    int minutes;
    public static Timer timer;
    public bool countTime = false;
    public int ms;
    public int s;
    [HideInInspector] public string time;

    private void Awake()
    {
        timer = this;
    }

    private void FixedUpdate()
    {
        if (countTime)
        {
            miliseconds += 20;
            ms += 20;
            if (miliseconds > 999)
            {
                seconds += 1;
                s += 1;
                miliseconds = 0;
            }
            if (seconds > 59)
            {
                minutes += 1;
                seconds = 0;
            }
            string mil = miliseconds.ToString();
            mil = mil.PadLeft(2, '0');
            mil = mil.Remove(mil.Length - 1);
            string sec = seconds.ToString();
            string min = minutes.ToString();
            time = min.PadLeft(2, '0') + ":" + sec.PadLeft(2, '0') + ":" + mil;
        }
    }
}
