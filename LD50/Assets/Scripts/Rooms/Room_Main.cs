using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Main : MonoBehaviour
{
    string OverWriteRoomSelection;
    private List<GameObject> rooms = new List<GameObject>();
    private List<string> roomNames = new List<string>();
    [HideInInspector] public string state;

    private bool canConfirm = true;
    private string lastRoom = "none";

    private void Awake()
    {
        OverWriteRoomSelection = GameState.gs.overWriteRoom;
        foreach (Transform child in this.gameObject.GetComponent<Transform>()) rooms.Add(child.gameObject);
        for (int i = 0; i < rooms.Count - 1; i++)
        {
            string[] splitted = new string[2];
            splitted = rooms[i].name.Split('_');
            roomNames.Add(splitted[1]);
        }
    }

    private void Start()
    {
        if (OverWriteRoomSelection == null || OverWriteRoomSelection == "")
        {
            int rdm = rdm = Random.Range(0, rooms.Count - 1);
            for (int i = 0; i < rooms.Count - 1; i++) rooms[i].SetActive(false);
            while (roomNames[rdm] == lastRoom)
            {
                rdm = Random.Range(0, rooms.Count - 1);
            }
            lastRoom = roomNames[rdm];
            rooms[rdm].SetActive(true);
            GameManager.gm.currRoomType = roomNames[rdm];
        }
        else
        {
            for (int i = 0; i < rooms.Count - 1; i++)
            {
                if (roomNames[i] == OverWriteRoomSelection)
                {
                    for (int k = 0; k < rooms.Count - 1; k++) rooms[k].SetActive(false);
                    rooms[i].SetActive(true);
                    GameManager.gm.currRoomType = roomNames[i];
                    break;
                }
            }
        }
    }

    IEnumerator ConfirmCool()
    {
        yield return new WaitForSeconds(.5f);
        canConfirm = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            this.state = "ok";
            OnConfirm(null);
        }
    }
    public void OnConfirm(GameObject but)
    {
        if (!canConfirm) return;
        canConfirm = false;
        but.GetComponent<Animator>().SetTrigger("isPressed");
        switch (state)
        {
            case "perfect":
                GameManager.gm.currTunnel.OpenDoor(0);
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleCorrect);
                break;
            case "ok":
                GameManager.gm.ceilingSpeed += GameManager.gm.speedBoost;
                GameManager.gm.currTunnel.OpenDoor(0);
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleWrong);
                AudioManager.am.FMOD_CeilingFasterOneShot();
                break;
            case "bad":
                StartCoroutine(ConfirmCool());
                GameManager.gm.currTunnel.CloseDoor(0);
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleFullWrong);
                break;
            default:
                StartCoroutine(ConfirmCool());
                GameManager.gm.currTunnel.CloseDoor(0);
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleFullWrong);
                break;

        }
    }
}