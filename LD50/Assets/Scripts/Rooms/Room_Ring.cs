using UnityEngine;

public class Room_Ring : MonoBehaviour
{
    public Slot[] s;
    public Questions[] Questions;
    private int ringsOnSide =0;
    private int randomQuestion;
    Room_Ring_Main main;

    private void Awake()
    {
        main = GetComponentInParent<Transform>().GetComponentInParent<Room_Ring_Main>();
        randomQuestion = Random.Range(0, Questions.Length);
    }

    private void Start()
    {
        GameManager.gm.whatRoom = "ring";
    }

    public void MoveDown()
    {
        int ringMovedIndex = 0;
        for (int j = s.Length - 1; j > 0; j--)
        {
            if (s[j].ring == null)
            {
                ringMovedIndex = j + 1;
                break;
            }
        }
        for (int j = 0; j < s.Length; j++)
        {
            if (s[j].ring == null)
            {
                s[j].ring = s[ringMovedIndex].ring;
                s[j].ring.transform.position = s[j].pos;
                s[ringMovedIndex].ring = null;
                if (Questions[randomQuestion].answer == ringsOnSide)
                {
                    if(main.correctSolutions == 2)
                    {
                        GameManager.gm.currTunnel.doorIn.GetComponent<Animator>().SetTrigger("isClosed");
                    }
                    main.correctSolutions--;

                }
                ringsOnSide--;
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
                ringMovedIndex = j - 1;
                break;
            }
        }
        for (int j = s.Length - 1; j > -1; j--)
        {
            if (s[j].ring == null)
            {
                s[j].ring = s[ringMovedIndex].ring;
                s[j].ring.transform.position = s[j].pos;
                s[ringMovedIndex].ring = null;
                ringsOnSide++;
                if(Questions[randomQuestion].answer == ringsOnSide)
                {
                    if(main.correctSolutions == 1)
                    {
                        GameManager.gm.currTunnel.doorIn.GetComponent<Animator>().SetTrigger("isOpen");
                    }
                    main.correctSolutions++;
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
