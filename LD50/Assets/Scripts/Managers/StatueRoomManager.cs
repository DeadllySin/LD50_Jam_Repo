using System.Collections.Generic;
using UnityEngine;

public class StatueRoomManager : MonoBehaviour
{
    public int correctPieces;
    List <GameObject> spawners = new List<GameObject>();
    [SerializeField] private GameObject[] spawner;
    [SerializeField] private GameObject statuePieces;
    [SerializeField] private GameObject statuePiece2;
    [SerializeField] private GameObject statuePiece3;
    [SerializeField] private Animator doorAnim;

    private void Awake()
    {
        for (int i = 0; i < spawner.Length; i++) spawners.Add(spawner[i]);
    }

    private void Start()
    {
        Debug.Log(spawners.Count);
        int temp = Random.Range(0, spawners.Count -1);
        Instantiate(statuePieces, spawners[temp].transform.position, Quaternion.identity);
        spawners.Remove(spawners[temp]);
        int temp2 = Random.Range(0, spawners.Count -1);
        Instantiate(statuePiece2, spawners[temp2].transform.position, Quaternion.identity);
        spawners.Remove(spawners[temp2]);
        int temp3 = Random.Range(0 , spawners.Count-1);
        Instantiate(statuePiece3, spawners[temp2].transform.position, Quaternion.identity);
        spawners.Remove(spawners[temp3]);
    }

    public void SpawnRoom()
    {
        GameManager.gm.currDoor.GetComponent<Animator>().SetTrigger("isOpen");
    }
}
