using UnityEngine;

public class Main_Room : MonoBehaviour
{
    public Rooms[] rooms;
    [SerializeField] private GameObject ceiling;

    private void Start()
    {
        ceiling.transform.parent = GameManager.gm.ceiling.transform;
        ceiling.transform.localPosition = new Vector3(0, 0, transform.position.z);
        for (int i = 0; i < rooms.Length; i++)
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
