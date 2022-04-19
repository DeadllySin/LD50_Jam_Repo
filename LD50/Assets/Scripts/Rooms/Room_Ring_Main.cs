using UnityEngine;
using UnityEngine.AI;

public class Room_Ring_Main : MonoBehaviour
{
    public bool[] solutionCorrect = new bool[3];
    int correctSolutions;
    private Main_Room room;
    EnemyAI ai;

    private void Start()
    {
        ai = GetComponentInChildren<EnemyAI>();
        room = GetComponentInParent<Main_Room>();
    }

    public void OnChanged()
    {
        int oldCorrectSol = correctSolutions;
        correctSolutions = 3;
        for(int i = 0; i < solutionCorrect.Length; i++)
        {
            if (solutionCorrect[i] == false) correctSolutions -= 1; 
        }

        if(oldCorrectSol < 2 && correctSolutions > 1)
        {
            GameManager.gm.currTunnel.doorIn.GetComponent<Animator>().SetTrigger("isOpen");
            ai.speed = 0;
        }
        else if(oldCorrectSol > 1 && correctSolutions < 2)
        {
            GameManager.gm.currTunnel.doorIn.GetComponent<Animator>().SetTrigger("isClosed");
            ai.speed = ai.defaultSpeed;
        }
        if(correctSolutions == 2)
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
