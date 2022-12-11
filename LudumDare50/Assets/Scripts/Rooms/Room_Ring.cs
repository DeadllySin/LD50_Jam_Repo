using System.Collections.Generic;
using UnityEngine;

public class Room_Ring : MonoBehaviour
{
    [SerializeField] private float[] y_pos;
    [SerializeField] private float x_pos, z_pos;
    [SerializeField] private GameObject[] symbol;
    [SerializeField] private Pole[] pole;
    private Room_Main main;
    private int maxSymbols;
    private int[] ringsOnSide = new int[2];
    private int randomQuestion;
    [HideInInspector] public bool[] solutionCorrect = new bool[2];
    [SerializeField] private TextAsset math;

    private void Start()
    {
        main = GetComponentInParent<Room_Main>();
        for (int p = 0; p < pole.Length; p++)
        {
            foreach (Transform child in pole[p].questionSpawnerParent) pole[p].questionSpawners.Add(child.gameObject.GetComponent<Transform>());
            switch (main.gm.ringRoomPro)
            {
                case 0:
                    maxSymbols = 3;
                    pole[p].questionSpawners.RemoveAt(pole[p].questionSpawners.Count - 1);
                    pole[p].questionSpawners.RemoveAt(pole[p].questionSpawners.Count - 1);
                    pole[p].questionSpawners.RemoveAt(0);
                    pole[p].questionSpawners.RemoveAt(0);
                    break;
                case 1:
                    maxSymbols = 3;
                    pole[p].questionSpawners.RemoveAt(pole[p].questionSpawners.Count - 1);
                    pole[p].questionSpawners.RemoveAt(pole[p].questionSpawners.Count - 1);
                    pole[p].questionSpawners.RemoveAt(0);
                    pole[p].questionSpawners.RemoveAt(0);
                    break;
                case 2:
                    maxSymbols = 5;
                    pole[p].questionSpawners.RemoveAt(pole[p].questionSpawners.Count - 1);
                    pole[p].questionSpawners.RemoveAt(0);
                    break;
                case 3:
                    maxSymbols = 5;
                    pole[p].questionSpawners.RemoveAt(pole[p].questionSpawners.Count - 1);
                    pole[p].questionSpawners.RemoveAt(0);
                    break;
                default:
                    maxSymbols = 7;
                    break;
            }

            print("test");

            string[] linesInFile = math.text.Split('\n');
            foreach (string line in linesInFile)
            {
                if (line.Length == maxSymbols + 3)
                {
                    string[] splites = line.Split('=');
                    pole[p].Questions.Add(splites[0]);
                    pole[p].Solutions.Add(splites[1]);
                }
            }
            randomQuestion = Random.Range(0, pole[p].Questions.Count - 1);
            randomQuestion = Random.Range(0, pole[p].Questions.Count - 1);
            randomQuestion = Random.Range(0, pole[p].Questions.Count - 1);
            randomQuestion = Random.Range(0, pole[p].Questions.Count - 1);
            pole[p].answer = pole[p].Solutions[randomQuestion];
            pole[p].question = pole[p].Questions[randomQuestion];
            int y = 0;
            for (int i = 0; i < pole[p].question.Length; i++)
            {
                for (int j = 0; j < symbol.Length; j++)
                {
                    if (char.Parse(symbol[j].name) == pole[p].question[i])
                    {
                        GameObject sym = Instantiate(symbol[j], pole[p].questionSpawners[y].position, Quaternion.identity);
                        sym.GetComponentInChildren<MeshRenderer>().material = pole[p].gem;
                        sym.transform.parent = pole[p].questionSpawners[y].parent;
                        if (p == 0) sym.transform.Rotate(0.0f, 90f, 0.0f, Space.World);
                        else sym.transform.Rotate(0.0f, 270f, 0.0f, Space.World);
                        sym.transform.position = pole[p].questionSpawners[y].position;
                        y++;
                    }
                }
            }
        }
        main.gm.ringRoomPro++;
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
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pSlideDown);
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
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pSlideUp);
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
                break;
            case 1:
                main.state = "ok";
                break;
            case 2:
                main.state = "perfect";
                break;
        }
    }
}

[System.Serializable]
public class Pole
{
    [HideInInspector] public int ringsOnCorrectSide;
    [HideInInspector] public string question;
    [HideInInspector] public string answer;
    [HideInInspector] public List<string> Questions = new List<string>();
    [HideInInspector] public List<string> Solutions = new List<string>();
    [HideInInspector] public List<Transform> questionSpawners = new List<Transform>();
    public Transform questionSpawnerParent;
    public Slot[] slot;
    public Material gem;


    [System.Serializable]
    public struct Slot
    {
        public GameObject ring;
    }
}