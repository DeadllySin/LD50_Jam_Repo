using UnityEngine;
using System.Collections;

public class Tunnel : MonoBehaviour
{
    bool alreadyColl;
    public GameObject doorIn;
    public GameObject doorOut;
    [SerializeField] private GameObject ceil;
    [SerializeField] private GameObject fakeDoor;
    [SerializeField] private GameObject tunnelPrefab;

    private void Update() { if (GameManager.gm.ceiling.transform.position.y <= ceil.transform.position.y) ceil.transform.parent = GameManager.gm.ceiling.transform; }

    public void NewRoom()
    {
        StartCoroutine(NewRoomEnu());
    }

    IEnumerator NewRoomEnu()
    {
        if (!alreadyColl)
        {
            alreadyColl = true;
            CloseDoor(doorIn);
            Room_Statue room = FindObjectOfType<Room_Statue>();
            Tunnel tunnel = GetComponentInParent<Transform>().GetComponentInParent<Tunnel>();
            switch (room.correctPieces)
            {
                case 2:
                    FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleWrong);
                    Debug.Log("Wrong Puzzle");
                    break;
                case 3:
                    FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleCorrect);
                    Debug.Log("Correct Puzzle");
                    break;
                default:
                    break;
            }
            Destroy(room.gameObject);
            int nextRoomIndex = Random.Range(0, GameManager.gm.roomList.Length - 1);
            while (nextRoomIndex == GameManager.gm.lastRoom) nextRoomIndex = Random.Range(0, GameManager.gm.roomList.Length);
            GameManager.gm.lastRoom = nextRoomIndex;
            GameManager.gm.currRoom = Instantiate(GameManager.gm.roomList[nextRoomIndex], new Vector3(0, 0, room.gameObject.transform.position.z + 22), Quaternion.identity);
            GameManager.gm.currTunnel = Instantiate(tunnelPrefab, new Vector3(0, 0, tunnel.gameObject.transform.position.z + 22), Quaternion.identity).GetComponent<Tunnel>();
            yield return new WaitForSeconds(2);
            OpenDoor(doorOut);
            IdleDoor(GameManager.gm.currTunnel.doorIn);
        }
    }

    public void RemoveTunnel()
    {
        StartCoroutine(RemoveTunnelEnu());
    }

    IEnumerator RemoveTunnelEnu()
    {
        CloseDoor(doorOut);
        GameObject tunnelParent = GetComponentInParent<Transform>().GetComponentInParent<Tunnel>().gameObject;
        yield return new WaitForSeconds(.8f);
        Vector3 fakeDoorPos = new Vector3(8.75f, 1, tunnelParent.GetComponent<Tunnel>().doorOut.transform.position.z);
        GameObject fakeDoorTemp = Instantiate(fakeDoor, fakeDoorPos, Quaternion.Euler(new Vector3(0, 90, 0)));
        fakeDoorTemp.transform.parent = GameManager.gm.currRoom.transform;
        Destroy(tunnelParent);
    }

    public void OpenDoor(GameObject door)
    {
        door.GetComponent<Animator>().SetTrigger("isOpen");
    }
    public void CloseDoor(GameObject door)
    {
        door.GetComponent<Animator>().SetTrigger("isClosed");
    }
    public void IdleDoor(GameObject door)
    {
        door.GetComponent<Animator>().SetTrigger("isIdle");
    } 
}
