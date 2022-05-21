using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Main : MonoBehaviour
{
    string OverWriteRoomSelection;
    [HideInInspector] public GameManager gm;
    private List<GameObject> rooms = new List<GameObject>();
    private List<string> roomNames = new List<string>();
    [HideInInspector] public string state;
    [SerializeField] private Interactable confirmBut;
    [SerializeField] private Color perfectCol;
    [SerializeField] private Color okCol;

    private bool canConfirm = true;
    private string lastRoom = "none";

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
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
            gm.currRoomType = roomNames[rdm];
        }
        else
        {
            for (int i = 0; i < rooms.Count - 1; i++)
            {
                if (roomNames[i] == OverWriteRoomSelection)
                {
                    for (int k = 0; k < rooms.Count - 1; k++) rooms[k].SetActive(false);
                    rooms[i].SetActive(true);
                    gm.currRoomType = roomNames[i];
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

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleWrong);
            this.state = "ok";
            OnConfirm();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleCorrect);
            this.state = "perfect";
            OnConfirm();
        }
    }*/

    public void OnConfirm()
    {
        if (!canConfirm) return;
        canConfirm = false;
        //FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pConfirm);

        switch (state)
        {
            case "perfect":
                confirmBut.GetComponentInChildren<Light>().color = perfectCol;
                confirmBut.arg = null;
                gm.currTunnel.OpenDoor(0);
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleCorrect);
                state = "bad";
                break;
            case "ok":
                confirmBut.GetComponentInChildren<Light>().color = okCol;
                confirmBut.arg = null;
                gm.ceilingSpeed += gm.speedBoost;
                gm.currTunnel.OpenDoor(0);
                //FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleFullWrong);
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleWrong);
                AudioManager.am.FMOD_CeilingFasterOneShot();
                state = "bad";
                break;
            default:
                confirmBut.arg = "light";
                StartCoroutine(ConfirmCool());
                gm.currTunnel.CloseDoor(0);
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleFullWrong);
                break;

        }
        if (confirmBut.arg == "light")
        {
            confirmBut.GetComponentInChildren<Light>().enabled = false;
            Invoke(nameof(Disablelight), .5f);
        }
        confirmBut.GetComponent<Animator>().SetTrigger("isPressed");
        
    }

    void Disablelight()
    {
        confirmBut.GetComponentInChildren<Light>().enabled = true;
    }
}