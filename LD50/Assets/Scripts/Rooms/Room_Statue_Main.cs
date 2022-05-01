using UnityEngine;

public class Room_Statue_Main : MonoBehaviour
{
    [SerializeField] private GameObject[] statueRooms;

    private void Awake()
    {
        if (GameManager.gm.statueRoomPro < 2)
        {
            statueRooms[0].SetActive(true);
        }
        else if (GameManager.gm.statueRoomPro < 4 && GameManager.gm.statueRoomPro > 1)
        {
            statueRooms[1].SetActive(true);
        }
        else if (GameManager.gm.statueRoomPro > 3)
        {
            statueRooms[2].SetActive(true);
        }

        GameManager.gm.statueRoomPro++;
    }
}
