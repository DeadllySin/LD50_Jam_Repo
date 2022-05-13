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
    private void Awake()
    {
        IdleOponDoor(2);
    }

    private void Update()
    {
        if (GameManager.gm.ceiling.transform.position.y <= ceil.transform.position.y) ceil.transform.parent = GameManager.gm.ceiling.transform;
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
            CloseDoor(0);

            yield return new WaitForSeconds(.5f);

            for (int i = 0; i < 3; i++) statue = FindObjectsOfType<Interactable_Statue>();
            Room_Main room = GameManager.gm.currRoom.GetComponent<Room_Main>();
            Tunnel tunnel = GameManager.gm.currTunnel.GetComponent<Tunnel>();
            GameObject ceiling = GameManager.gm.ceiling;
            float roomPos = room.gameObject.transform.position.z + 22;
            GameManager.gm.roomsCleared++;
            Vector3 newCelPos = new Vector3(ceiling.transform.position.x, ceiling.transform.position.y, ceiling.transform.position.z + 22);
            GameManager.gm.ceiling.transform.position = newCelPos;

            yield return new WaitForSeconds(.1f);

            for (int i = 0; i < statue.Length; i++) Destroy(statue[i].gameObject);
            Destroy(room.gameObject);

            yield return new WaitForSeconds(.1f);

            GameManager.gm.currRoom = Instantiate(GameManager.gm.room, new Vector3(0, 0, roomPos), Quaternion.identity);
            GameManager.gm.currTunnel = Instantiate(tunnelPrefab, new Vector3(0, 0, tunnel.gameObject.transform.position.z + 22), Quaternion.identity).GetComponent<Tunnel>();

            yield return new WaitForSeconds(.5f);
            IdleDoor(0);
            OpenDoor(1);
            AudioManager.am.tunnelOcclusionSSInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    public void RemoveTunnel()
    {
        StartCoroutine(RemoveTunnelEnu());
    }

    IEnumerator RemoveTunnelEnu()
    {
        CloseDoor(2);
        GameObject tunnelParent = GetComponentInParent<Transform>().GetComponentInParent<Tunnel>().gameObject;
        yield return new WaitForSeconds(.8f);
        Vector3 fakeDoorPos = new Vector3(8.75f, 1, tunnelParent.GetComponent<Tunnel>().door[1].door.gameObject.transform.position.z + 0.5f);
        GameObject fakeDoorTemp = Instantiate(fakeDoor, fakeDoorPos, Quaternion.Euler(new Vector3(0, 90, 0)));
        fakeDoorTemp.transform.parent = GameManager.gm.currRoom.transform;
        Destroy(tunnelParent);
    }

    #region DoorAnimations
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
        door[index].door.SetTrigger("isClosed");
        door[index].isOpen = false;
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.doorClose, door[index].door.transform.position);
    }
    public void IdleDoor(int index)
    {
        door[index].door.SetTrigger("isIdle");
        door[index].isOpen = true;
    }

    public void IdleOponDoor(int index)
    {
        door[index].door.SetTrigger("isIdleOpen");
        door[index].isOpen = true;
    }
    #endregion
}

[System.Serializable]
public struct Door
{
    public Animator door;
    [HideInInspector] public bool isOpen;
}
