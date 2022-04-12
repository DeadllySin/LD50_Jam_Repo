using System.Collections;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private Animator door1;
    [SerializeField] private Animator door2;
    [SerializeField] private GameObject nextRoom;
    bool alreadyColl;

    public IEnumerator newRoom()
    {
        if (!alreadyColl)
        {
            door1.SetTrigger("isClosed");
            StatueRoomManager room = FindObjectOfType<StatueRoomManager>();
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
            Instantiate(nextRoom, new Vector3(0, 0, room.gameObject.transform.position.z + 22), Quaternion.identity);
            yield return new WaitForSeconds(1);
            door2.SetTrigger("isOpen");
            alreadyColl = true;
        }
    }
}
