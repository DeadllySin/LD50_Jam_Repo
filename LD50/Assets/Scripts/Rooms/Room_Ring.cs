using UnityEngine;

public class Room_Ring : MonoBehaviour
{
    private int ringsOnSide = 0;
    private int randomQuestion;
    private string question;
    private string answer;
    private Room_Ring_Main main;
    [SerializeField] private int whichPole;
    [SerializeField] private Slot[] slot;
    [SerializeField] private Transform[] questionSpawners;

    private void Start()
    {
        main = GetComponentInParent<Room_Ring_Main>();
        randomQuestion = Random.Range(0, main.Questions.Count - 1);
        answer = main.Solutions[randomQuestion];
        question = main.Questions[randomQuestion];
        for (int i = 0; i < question.Length; i++)
        {
            for (int j = 0; j < main.Symbols.Length; j++)
            {
                if (main.Symbols[j].symbolName == question[i])
                {
                    Instantiate(main.Symbols[j].symbol, questionSpawners[i].position, Quaternion.identity);
                }
            }
        }
    }

    public void MoveDown()
    {
        int ringMovedIndex = 0;
        for (int j = slot.Length - 1; j > 0; j--)
        {
            if (slot[j].ring == null)
            {
                if (j == slot.Length - 1) return;
                ringMovedIndex = j + 1;
                break;
            }
        }
        for (int j = 0; j < slot.Length; j++)
        {
            if (slot[j].ring == null)
            {
                slot[j].ring = slot[ringMovedIndex].ring;
                slot[j].ring.transform.localPosition = slot[j].pos;
                slot[ringMovedIndex].ring = null;
                ringsOnSide--;
                if (int.Parse(answer) == ringsOnSide) main.solutionCorrect[whichPole] = true;
                else main.solutionCorrect[whichPole] = false;
                return;
            }
        }
    }

    public void MoveUp()
    {
        int ringMovedIndex = 0;
        for (int j = 0; j < slot.Length; j++)
        {
            if (slot[j].ring == null)
            {
                if (j == 0) return;
                ringMovedIndex = j - 1;
                break;
            }
        }
        for (int j = slot.Length - 1; j > -1; j--)
        {
            if (slot[j].ring == null)
            {
                slot[j].ring = slot[ringMovedIndex].ring;
                slot[j].ring.transform.localPosition = slot[j].pos;
                slot[ringMovedIndex].ring = null;
                ringsOnSide++;
                if (int.Parse(answer) == ringsOnSide) main.solutionCorrect[whichPole] = true;
                else main.solutionCorrect[whichPole] = false;
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