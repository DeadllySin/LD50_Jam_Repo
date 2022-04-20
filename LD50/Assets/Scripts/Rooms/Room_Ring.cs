using System.Collections.Generic;
using UnityEngine;
public class Room_Ring : MonoBehaviour
{
    public Slot[] s;
    public Questions[] Questions;
    private int ringsOnSide = 0;
    private int randomQuestion;
    Room_Ring_Main main;
    [SerializeField] private int which;
    private List<char> quesCh = new List<char>();

    private void Start()
    {
        main = GetComponentInParent<Room_Ring_Main>();
        randomQuestion = Random.Range(0, Questions.Length);
        GenerateQuestion();
    }

    void GenerateQuestion()
    {
        string ques = "";
        int qzuesInt = 0;
        ques = Random.Range(1, 5).ToString() + Random.Range(8, 9).ToString() + Random.Range(1, 5).ToString() + Random.Range(8, 9).ToString() + Random.Range(1, 5).ToString();
        for (int i = 0; i < ques.Length; i++)
        {
            quesCh.Add(ques[i]);
        }
        qzuesInt += int.Parse(quesCh[0].ToString());
        if (quesCh[1] == 8)
        {
            qzuesInt += int.Parse(quesCh[2].ToString());
            if (quesCh[3] == 8)
            {
                qzuesInt += int.Parse(quesCh[4].ToString());
            }
            else if (quesCh[3] == 9)
            {
                qzuesInt -= int.Parse(quesCh[4].ToString());
            }
        }
        else if (quesCh[1] == 9)
        {
            qzuesInt -= int.Parse(quesCh[2].ToString());
            if (quesCh[3] == 8)
            {
                qzuesInt += int.Parse(quesCh[4].ToString());
            }
            else if (quesCh[3] == 9)
            {
                qzuesInt -= int.Parse(quesCh[4].ToString());
            }
        }
        string questionFinal = "";
        for (int i = 0; i < ques.Length; i++)
        {
            if (ques[i] == '8') questionFinal += "+";
            else if (ques[i] == '9') questionFinal += "-";
            else questionFinal += ques[i];
        }

        Debug.Log(questionFinal + "  " + ques + "  " + qzuesInt);
    }


    public void MoveDown()
    {
        int ringMovedIndex = 0;
        for (int j = s.Length - 1; j > 0; j--)
        {
            if (s[j].ring == null)
            {
                if (j == s.Length - 1) return;
                ringMovedIndex = j + 1;
                break;
            }
        }
        for (int j = 0; j < s.Length; j++)
        {
            if (s[j].ring == null)
            {
                s[j].ring = s[ringMovedIndex].ring;
                s[j].ring.transform.localPosition = s[j].pos;
                s[ringMovedIndex].ring = null;
                ringsOnSide--;
                if (Questions[randomQuestion].answer == ringsOnSide)
                {
                    main.solutionCorrect[which] = true;
                }
                else
                {
                    main.solutionCorrect[which] = false;
                }
                return;
            }
        }
    }

    public void MoveUp()
    {
        int ringMovedIndex = 0;
        for (int j = 0; j < s.Length; j++)
        {
            if (s[j].ring == null)
            {
                if (j == 0) return;
                ringMovedIndex = j - 1;
                break;
            }
        }
        for (int j = s.Length - 1; j > -1; j--)
        {
            if (s[j].ring == null)
            {
                s[j].ring = s[ringMovedIndex].ring;
                s[j].ring.transform.localPosition = s[j].pos;
                s[ringMovedIndex].ring = null;
                ringsOnSide++;
                if (Questions[randomQuestion].answer == ringsOnSide)
                {
                    main.solutionCorrect[which] = true;
                }
                else
                {
                    main.solutionCorrect[which] = false;
                }
                return;
            }
        }
    }
}

[System.Serializable]
public struct Slot
{
    public GameObject ring;
    public Vector3 pos;
}

[System.Serializable]
public struct Questions
{
    public string question;
    public int answer;
}
