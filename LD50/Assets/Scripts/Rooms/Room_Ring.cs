using System.Collections.Generic;
using UnityEngine;

public class Room_Ring : MonoBehaviour
{
    public Slot[] s;
    public Questions[] Questions;
    private int ringsOnSide = 0;
    private int randomQuestion;
    Room_Ring_Main main;
    [SerializeField] private Transform[] questionSpawners;
    [SerializeField] private int which;

    private void Start()
    {
        main = GetComponentInParent<Room_Ring_Main>();
        randomQuestion = Random.Range(0, Questions.Length);

        for(int i = 0; i < Questions[randomQuestion].question.Length; i++)
        {
            for(int j = 0; j < main.Symbols.Length; j++)
            {
                if(main.Symbols[j].symbolName == Questions[randomQuestion].question[i])
                {
                    Instantiate(main.Symbols[j].symbol, questionSpawners[i].position, Quaternion.identity);
                }
            }
        }
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
                if (Questions[randomQuestion].answer == ringsOnSide) main.solutionCorrect[which] = true;
                else main.solutionCorrect[which] = false;
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
                if (Questions[randomQuestion].answer == ringsOnSide) main.solutionCorrect[which] = true;
                else main.solutionCorrect[which] = false;
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


