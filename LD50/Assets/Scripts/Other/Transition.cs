using System.Collections;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private Animator door1;
    [SerializeField] private Animator door2;
    [SerializeField] private GameObject nextRoom;
    [SerializeField] private GameObject nextTunnel;
    bool alreadyColl;

    public void doNewRoom()
    {
        StartCoroutine(newRoom());
    }

    IEnumerator newRoom()
    {
        if (!alreadyColl)
        {
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
            int nextRoomIndex = Random.Range(0,GameManager.gm.roomList.Length);
            while(nextRoomIndex == GameManager.gm.lastRoom)
            {
                nextRoomIndex = Random.Range(0, GameManager.gm.roomList.Length);
            }
            GameManager.gm.currRoom = Instantiate(nextRoom, new Vector3(0, 0, room.gameObject.transform.position.z + 22), Quaternion.identity);
            GameManager.gm.currDoor = Instantiate(GameManager.gm.roomList[nextRoomIndex], new Vector3(0, 0, tunnel.gameObject.transform.position.z + 22), Quaternion.identity).GetComponent<Tunnel>().doorIn;
            yield return new WaitForSeconds(1);
            Destroy(room.gameObject);
            door2.SetTrigger("isOpen");
            alreadyColl = true;
        }
    }
}
