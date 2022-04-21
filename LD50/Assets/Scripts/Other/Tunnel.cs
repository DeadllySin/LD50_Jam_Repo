using System.Collections;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    bool alreadyColl;
    public Door[] door;
    Interactable_Statue[] statue = new Interactable_Statue[3];
    [SerializeField] private GameObject ceil;
    [SerializeField] private GameObject fakeDoor;
    [SerializeField] private GameObject tunnelPrefab;

    private void Update()
    {
        if (GameManager.gm.ceiling.transform.position.y <= ceil.transform.position.y) ceil.transform.parent = GameManager.gm.ceiling.transform;

        if (Input.GetKeyDown("h"))
        {
            OpenDoor(0);
        }
        if (Input.GetKeyDown("j"))
        {
            CloseDoor(0);
        }

    }

    public void NewRoom()
    {
        StartCoroutine(NewRoomEnu());
    }

    IEnumerator NewRoomEnu()
    {
        if (!alreadyColl)
        {
            for (int i = 0; i < 3; i++) statue = FindObjectsOfType<Interactable_Statue>();
            yield return new WaitForSeconds(.1f);

            for (int i = 0; i < statue.Length; i++)
            {
                Destroy(statue[i].gameObject);
            }
            yield return new WaitForSeconds(.1f);
            alreadyColl = true;
            CloseDoor(0);
            Room_Main room = GameManager.gm.currRoom.GetComponent<Room_Main>();
            Tunnel tunnel = GameManager.gm.currTunnel.GetComponent<Tunnel>();
            yield return new WaitForSeconds(1);
            switch (room.winState)
            {
                case "normal":
                    FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleWrong);
                    Debug.Log("Wrong Puzzle");
                    break;
                case "good":
                    FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleCorrect);
                    Debug.Log("Correct Puzzle");
                    break;
                default:
                    break;
            }
            Destroy(room.gameObject);
            GameManager.gm.ceiling.transform.position = new Vector3(GameManager.gm.ceiling.transform.position.x, GameManager.gm.ceiling.transform.position.y, GameManager.gm.ceiling.transform.position.z + 22);
            GameManager.gm.currRoom = Instantiate(GameManager.gm.room, new Vector3(0, 0, room.gameObject.transform.position.z + 22), Quaternion.identity);
            GameManager.gm.currTunnel = Instantiate(tunnelPrefab, new Vector3(0, 0, tunnel.gameObject.transform.position.z + 22), Quaternion.identity).GetComponent<Tunnel>();
            yield return new WaitForSeconds(1);
            OpenDoor(1);
            IdleDoor(0);
        }
    }

    public void RemoveTunnel()
    {
        StartCoroutine(RemoveTunnelEnu());
    }

    IEnumerator RemoveTunnelEnu()
    {
        CloseDoor(1);
        GameObject tunnelParent = GetComponentInParent<Transform>().GetComponentInParent<Tunnel>().gameObject;
        yield return new WaitForSeconds(.8f);
        Vector3 fakeDoorPos = new Vector3(8.75f, 1, tunnelParent.GetComponent<Tunnel>().door[1].door.gameObject.transform.position.z);
        GameObject fakeDoorTemp = Instantiate(fakeDoor, fakeDoorPos, Quaternion.Euler(new Vector3(0, 90, 0)));
        fakeDoorTemp.transform.parent = GameManager.gm.currRoom.transform;
        Destroy(tunnelParent);
    }

    public void OpenDoor(int index)
    {
        if (door[index].isOpen) return;
        door[index].door.SetTrigger("isOpen");
        door[index].isOpen = true;
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.doorOpen, door[index].door.transform.position);
    }
    public void CloseDoor(int index)
    {
        if (!door[index].isOpen) return;
        door[index].door.SetTrigger("isOpen");
        door[index].isOpen = true;
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.doorClose, door[index].door.transform.position);
    }
    public void IdleDoor(int index)
    {
        door[index].door.SetTrigger("isOpen");
        door[index].isOpen = true;
    }
}

[System.Serializable]
public struct Door
{
    public Animator door;
    public bool isOpen;
}
