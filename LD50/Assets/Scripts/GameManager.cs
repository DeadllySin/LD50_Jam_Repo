using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject room;

    public void SpawnRoom(GameObject spawner)
    {
        Instantiate(room, new Vector3(spawner.transform.position.x, spawner.transform.position.y, spawner.transform.position.z + 10),Quaternion.identity);
    }
}
