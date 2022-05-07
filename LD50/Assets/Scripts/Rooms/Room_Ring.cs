using System.Collections.Generic;
using UnityEngine;

public class Room_Ring : MonoBehaviour
{
    [SerializeField] private float[] y_pos;
    [SerializeField] private float x_pos, z_pos;
    [SerializeField] private GameObject[] symbol;
    [SerializeField] private Pole[] pole;
    private Room_Main main;
    [ SerializeField] private EnemyAI ai;
    private int maxSymbols;
    private int[] ringsOnSide = new int[2];
    private int randomQuestion;
    [HideInInspector] public bool[] solutionCorrect = new bool[2];

    private void Awake()
    {
        switch (GameManager.gm.ringRoomPro)
        {
            case 0:
                maxSymbols = 5;
                break;
            case 1:
                maxSymbols = 5;
                break;
            case 2:
                maxSymbols = 7;
                break;
            case 3:
                maxSymbols = 7;
                break;
            case 4:
                maxSymbols = 9;
                break;
            case 5:
                maxSymbols = 9;
                break;

        }
        GameManager.gm.ringRoomPro++;
        main = GetComponentInParent<Room_Main>();
        foreach (string line in System.IO.File.ReadLines(Application.dataPath + "/Math.txt"))
        {
            if (line.Length >= 9) break;
            string lineTemp = line;
            string[] splites;
            splites = lineTemp.Split('=');
            pole[0].Questions.Add(splites[0]);
            pole[0].Solutions.Add(splites[1]);
        }
        foreach (string line in System.IO.File.ReadLines(Application.dataPath + "/Math.txt"))
        {
            if (line.Length >= 9) break;
            string lineTemp = line;
            string[] splites;
            splites = lineTemp.Split('=');
            pole[1].Questions.Add(splites[0]);
            pole[1].Solutions.Add(splites[1]);
        }
        for (int r = 0; r < pole.Length; r++)
        {
            randomQuestion = Random.Range(0, pole[r].Questions.Count - 1);
            pole[r].answer = pole[r].Solutions[randomQuestion];
            pole[r].question = pole[r].Questions[randomQuestion];
            while (pole[r].question.Length > maxSymbols)
            {
                randomQuestion = Random.Range(0, pole[r].Questions.Count - 1);
                pole[r].answer = pole[r].Solutions[randomQuestion];
                pole[r].question = pole[r].Questions[randomQuestion];
            }

            int y = 0;
            for (int i = 0; i < pole[r].question.Length; i++)
            {
                for (int j = 0; j < symbol.Length; j++)
                {
                    if (char.Parse(symbol[j].name) == pole[r].question[i])
                    {
                        GameObject sym = Instantiate(symbol[j], pole[r].questionSpawners[y].position, Quaternion.identity);
                        sym.transform.parent = this.transform;
                        sym.transform.localScale = new Vector3(-1, 1, 1);
                        sym.transform.Rotate(0.0f, 270f, 0.0f, Space.World);
                        sym.transform.localPosition = pole[r].questionSpawners[y].position;
                        y++;
                    }
                }
            }
        }
    }

    public void MoveDown(int poleIndex)
    {
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
                pole[poleIndex].slot[j].ring.transform.localPosition = new Vector3(x_pos, y_pos[j], z_pos);
                pole[poleIndex].slot[ringMovedIndex].ring = null;
                ringsOnSide[poleIndex] -= 1;
                Debug.Log(int.Parse(pole[poleIndex].answer) + " " + ringsOnSide[poleIndex]);
                if (int.Parse(pole[poleIndex].answer) == ringsOnSide[poleIndex]) solutionCorrect[poleIndex] = true;
                else solutionCorrect[poleIndex] = false;
                OnValueChanged();
                return;
            }
        }
    }

    public void MoveUp(int poleIndex)
    {
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
        for (int j = pole[poleIndex].slot.Length - 1; j > -1; j--)
        {
            if (pole[poleIndex].slot[j].ring == null)
            {
                pole[poleIndex].slot[j].ring = pole[poleIndex].slot[ringMovedIndex].ring;
                pole[poleIndex].slot[j].ring.transform.localPosition = new Vector3(x_pos, y_pos[j], z_pos);
                pole[poleIndex].slot[ringMovedIndex].ring = null;
                ringsOnSide[poleIndex] += 1;

                if (int.Parse(pole[poleIndex].answer) == ringsOnSide[poleIndex]) solutionCorrect[poleIndex] = true;
                else solutionCorrect[poleIndex] = false;
                OnValueChanged();
                return;
            }
        }
    }

    public void OnValueChanged()
    {
        int correctSolutions = 3;
        for (int i = 0; i < solutionCorrect.Length; i++) if (solutionCorrect[i] == false) correctSolutions -= 1;
        Debug.Log(correctSolutions);
        switch (correctSolutions)
        {
            case 0:
                main.state = "bad";
                ai.speed = 2;
                break;
            case 1:
                main.state = "ok";
                ai.speed = 0;
                break;
            case 2:
                ai.speed = 0;
                main.state = "perfect";
                break;
        }
    }
}

[System.Serializable]
public class Pole
{
    [HideInInspector] public string question;
    [HideInInspector] public string answer;
    [HideInInspector] public List<string> Questions = new List<string>();
    [HideInInspector] public List<string> Solutions = new List<string>();
    public Transform[] questionSpawners;
    public Slot[] slot;
    [HideInInspector] public int ringsOnCorrectSide;

    [System.Serializable]
    public struct Slot
    {
        public GameObject ring;
    }
}