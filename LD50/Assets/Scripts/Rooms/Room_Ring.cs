using System.Collections.Generic;
using UnityEngine;

public class Room_Ring : MonoBehaviour
{
    private int ringsOnSide = 0;
    private int randomQuestion;
    private string question;
    private string answer;
    [SerializeField] private int whichPole;

    [SerializeField] private Transform[] questionSpawners;
    private Room_Main room;
    private EnemyAI ai;
    private int correctSolutions;
    [HideInInspector] public bool puzzleFeedback = true;
    [HideInInspector] public bool[] solutionCorrect = new bool[3];
    [HideInInspector] public List<string> Questions = new List<string>();
    [HideInInspector] public List<string> Solutions = new List<string>();
    public Symbols[] Symbols;
    public Pole[] pole;

    private void Start()
    {
        ai = GetComponentInChildren<EnemyAI>();
        room = GetComponentInParent<Room_Main>();
        randomQuestion = Random.Range(0, Questions.Count - 1);
        answer = Solutions[randomQuestion];
        question = Questions[randomQuestion];
        int y = 0;
        for (int i = 0; i < question.Length; i++)
        {
            for (int j = 0; j < Symbols.Length; j++)
            {
                if (Symbols[j].symbolName == question[i])
                {
                    GameObject sym = Instantiate(Symbols[j].symbol, questionSpawners[y].position, Quaternion.identity);
                    sym.transform.localScale = new Vector3(-1, 1, 1);
                    y++;
                }
            }
        }
        Debug.Log(question + " " + answer);
    }

    public void MoveDown(int poleIndex)
    {
        Debug.Log("ringdown");
        int ringMovedIndex = 0;
        for (int j = pole[poleIndex].slot.Length - 1; j > 0; j--)
        {
            if (pole[poleIndex].slot[j].ring == null)
            {
                if (j == pole[poleIndex].slot.Length - 1) return;
                ringMovedIndex = j + 1;
                break;
            }
        }
        for (int j = 0; j < pole[poleIndex].slot.Length; j++)
        {
            if (pole[poleIndex].slot[j].ring == null)
            {
                pole[poleIndex].slot[j].ring = pole[poleIndex].slot[ringMovedIndex].ring;
                pole[poleIndex].slot[j].ring.transform.localPosition = pole[poleIndex].slot[j].pos;
                pole[poleIndex].slot[ringMovedIndex].ring = null;
                ringsOnSide--;
                if (int.Parse(answer) == ringsOnSide) solutionCorrect[whichPole] = true;
                else solutionCorrect[whichPole] = false;
                return;
            }
        }
    }

    public void MoveUp(int poleIndex)
    {
        Debug.Log("ringup");
        int ringMovedIndex = 0;
        for (int j = 0; j < pole[poleIndex].slot.Length; j++)
        {
            if (pole[poleIndex].slot[j].ring == null)
            {
                if (j == 0) return;
                ringMovedIndex = j - 1;
                break;
            }
        }
        Debug.Log("ringup2");
        for (int j = pole[poleIndex].slot.Length - 1; j > -1; j--)
        {
            if (pole[poleIndex].slot[j].ring == null)
            {
                pole[poleIndex].slot[j].ring = pole[poleIndex].slot[ringMovedIndex].ring;
                pole[poleIndex].slot[j].ring.transform.localPosition = pole[poleIndex].slot[j].pos;
                pole[poleIndex].slot[ringMovedIndex].ring = null;
                ringsOnSide++;
                Debug.Log("ringup3");
                if (int.Parse(answer) == ringsOnSide) solutionCorrect[whichPole] = true;
                else solutionCorrect[whichPole] = false;
                return;
            }
        }
    }


    private void Awake()
    {
        int counter = 0;
        foreach (string line in System.IO.File.ReadLines(Application.dataPath + "/Math.txt"))
        {
            string lineTemp = line;
            string[] splites;
            splites = lineTemp.Split('=');

            Questions.Add(splites[0]);
            Solutions.Add(splites[1]);
            //Debug.Log(line);
            counter++;
        }
    }

    public void OnChanged()
    {
        correctSolutions = 3;
        for (int i = 0; i < solutionCorrect.Length; i++) if (solutionCorrect[i] == false) correctSolutions -= 1;
        if (correctSolutions > 1)
        {
            //ai.speed = 0;
            GameManager.gm.currTunnel.OpenDoor(0);
        }
        else if (correctSolutions < 2)
        {
            GameManager.gm.currTunnel.CloseDoor(0);
            //ai.speed = ai.defaultSpeed;
        }
        if (correctSolutions == 2)
        {
            room.winState = "normal";
            if (puzzleFeedback == true)
            {
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleWrong);
                puzzleFeedback = false;
            }
        }
        if (correctSolutions < 2)
        {
            room.winState = "bad";
            if (puzzleFeedback == true)
            {
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleWrong);
                puzzleFeedback = false;
            }
        }
        else if (correctSolutions == 3)
        {
            room.winState = "good";
            if (puzzleFeedback == true)
            {
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleCorrect);
                puzzleFeedback = false;
            }
        }
    }
}

[System.Serializable]
public class Symbols
{
    public GameObject symbol;
    public char symbolName;
}

[System.Serializable]
public struct Pole
{
    public Slot[] slot;
    public int ringsOnCorrectSide;

    [System.Serializable]
    public struct Slot
    {
        public GameObject ring;
        public Vector3 pos;
    }
}

