using UnityEngine;

public class Main_Room : MonoBehaviour
{
    public Rooms[] rooms;

    private void Start()
    {
        for(int i = 0; i < rooms.Length; i++)
        {
            rooms[i].room.SetActive(false);
        }
        int rdm = Random.Range(0, rooms.Length);
        rooms[rdm].room.SetActive(true);
        GameManager.gm.currRoomType = rooms[rdm].whichRoom;
    }
}

[System.Serializable]
public class Rooms
{
    public string whichRoom;
    public GameObject room;
}
