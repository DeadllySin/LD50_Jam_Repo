using System.Collections;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private Animator door1;
    [SerializeField] private Animator door2;
    [SerializeField] private GameObject nextRoom;
    [SerializeField] private GameObject nextTunnel;
    bool alreadyColl;

    public void CallNewRoom()
    {
        StartCoroutine(NewRoom());
    }

    IEnumerator NewRoom()
    {
        if (!alreadyColl)
        {
            alreadyColl = true;
            door1.SetTrigger("isClosed");
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
            int nextRoomIndex = Random.Range(0,GameManager.gm.roomList.Length - 1);
            while(nextRoomIndex == GameManager.gm.lastRoom) nextRoomIndex = Random.Range(0, GameManager.gm.roomList.Length);
            GameManager.gm.lastRoom = nextRoomIndex;
            GameManager.gm.currRoom = Instantiate(GameManager.gm.roomList[nextRoomIndex], new Vector3(0, 0, room.gameObject.transform.position.z + 22), Quaternion.identity);
            GameManager.gm.currDoor = Instantiate(nextTunnel, new Vector3(0, 0, tunnel.gameObject.transform.position.z + 22), Quaternion.identity).GetComponent<Tunnel>().doorIn;
            yield return new WaitForSeconds(2);
            Destroy(room.gameObject);
            door2.SetTrigger("isOpen");
        }
    }
}