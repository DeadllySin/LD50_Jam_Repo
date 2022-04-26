using UnityEngine;
using System.Collections.Generic;

public class Room_Main : MonoBehaviour
{
    [Tooltip("currently available rooms: color, ring, statue")] [SerializeField] string OverWriteRoomSelection;
    private List <GameObject> rooms = new List<GameObject>();
    private List <string> roomNames = new List<string>();
    [HideInInspector] public string winState;

    private void Start()
    {
        foreach (Transform child in this.gameObject.GetComponent<Transform>()) rooms.Add(child.gameObject);
        for (int i = 0; i < rooms.Count - 1; i++)
        {
            string[] splitted = new string[2];
            splitted = rooms[i].name.Split('_');
            roomNames.Add(splitted[1]);

        }
        if (OverWriteRoomSelection == null || OverWriteRoomSelection == "")
        {
            for (int i = 0; i < rooms.Count - 1; i++) rooms[i].SetActive(false);
            int rdm = Random.Range(0, rooms.Count - 1);
            rooms[rdm].SetActive(true);
            GameManager.gm.currRoomType = roomNames[rdm];
        }
        else
        {
            for (int i = 0; i < rooms.Count - 1; i++)
            {
                Debug.Log(roomNames[i] + OverWriteRoomSelection);
                if (roomNames[i] == OverWriteRoomSelection)
                {

                    for (int k = 0; k < rooms.Count - 1; k++) rooms[k].SetActive(false);
                    rooms[i].SetActive(true);
                    GameManager.gm.currRoomType = roomNames[i];
                    break;
                }
            }
        }
    }
}