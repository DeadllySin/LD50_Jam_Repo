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

    private void Awake()
    {
        CloseDoor(doorIn);
    }

    private void Update()
    {
        if(GameManager.gm.ceiling.transform.position.y <= ceil.transform.position.y)
        {
            ceil.transform.parent = GameManager.gm.ceiling.transform;
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
            alreadyColl = true;
            CloseDoor(doorIn);
            StatueRoomManager room = FindObjectOfType<StatueRoomManager>();
            Tunnel tunnel = GetComponentInParent<Transform>().GetComponentInParent<Tunnel>();
            switch (room.correctPieces)
            {
                case 2:
                    Debug.Log("2 Pieces correct. Put the code for the sound and ceiling here");
                    break;
                case 3:
                    Debug.Log("3 Pieces correct. Put the code for the sound and ceiling here");
                    break;
                default:
                    break;
            }
            int nextRoomIndex = Random.Range(0, GameManager.gm.roomList.Length - 1);
            while (nextRoomIndex == GameManager.gm.lastRoom) nextRoomIndex = Random.Range(0, GameManager.gm.roomList.Length);
            GameManager.gm.lastRoom = nextRoomIndex;
            GameManager.gm.currRoom = Instantiate(GameManager.gm.roomList[nextRoomIndex], new Vector3(0, 0, room.gameObject.transform.position.z + 22), Quaternion.identity);
            GameManager.gm.currDoor = Instantiate(tunnelPrefab, new Vector3(0, 0, tunnel.gameObject.transform.position.z + 22), Quaternion.identity).GetComponent<Tunnel>().doorIn;
            yield return new WaitForSeconds(2);
            Destroy(room.gameObject);
            OpenDoor(doorOut);
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
        GameObject fakeDoorTemp = Instantiate(fakeDoor, new Vector3(8.75f, 1, tunnelParent.GetComponent<Tunnel>().doorOut.transform.position.z), Quaternion.Euler(new Vector3(0, 90, 0)));
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
}
