using System.Collections.Generic;
using UnityEngine;

public class Room_Ring_Main : MonoBehaviour
{
    public bool[] solutionCorrect = new bool[3];
    int correctSolutions;
    private Main_Room room;
    EnemyAI ai;
    public List<string> Questions = new List<string>();
    public List<string> Solutions = new List<string>();
    public Symbols[] Symbols;

    private void Awake()
    {
        int counter = 0;
        foreach (string line in System.IO.File.ReadLines("Assets/Resources/test.txt"))
        {
            string lineTemp = line;
            string[] splites;
            splites = lineTemp.Split('=');

            Questions.Add(splites[0]);
            Solutions.Add(splites[1]);
            Debug.Log(line);
            counter++;
        }
    }

    private void Start()
    {
        ai = GetComponentInChildren<EnemyAI>();
        room = GetComponentInParent<Main_Room>();
    }

    public void OnChanged()
    {
        correctSolutions = 3;
        for (int i = 0; i < solutionCorrect.Length; i++)
        {
            if (solutionCorrect[i] == false) correctSolutions -= 1;
        }
        if(correctSolutions > 1)
        {
            ai.speed = 0;
            GameManager.gm.currTunnel.OpenDoor(0);
        }
        else if(correctSolutions < 2)
        {
            GameManager.gm.currTunnel.CloseDoor(0);
            ai.speed = ai.defaultSpeed;
        }
        if (correctSolutions == 2)
        {
            room.winState = "normal";
        }
        if (correctSolutions < 2)
        {
            room.winState = "bad";
        }
        else if (correctSolutions == 3)
        {
            room.winState = "good";
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
public struct Questions
{
    public string question;
    public int answer;
}