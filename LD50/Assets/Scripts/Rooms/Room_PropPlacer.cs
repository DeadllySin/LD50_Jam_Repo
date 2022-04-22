using System.Collections.Generic;
using UnityEngine;

public class Room_PropPlacer : MonoBehaviour
{
    [SerializeField] private GameObject[] propMeshess;
    readonly List<GameObject> spawners = new List<GameObject>();
    readonly List<GameObject> propMeshes = new List<GameObject>();
    [SerializeField] private GameObject spawnerParent;
    private Room_Main room;

    private void Awake()
    {
        room = GetComponentInParent<Room_Main>();
        for (int i = 0; i < propMeshess.Length; i++) propMeshes.Add(propMeshess[i]);
        foreach (Transform child in spawnerParent.transform) spawners.Add(child.gameObject);

        Debug.Log(spawners.Count);
    }

    private void Start()
    {
        while (propMeshes.Count > 0)
        {
            int spawnersIndex = Random.Range(0, spawners.Count);
            int propIndex = Random.Range(0, propMeshes.Count);
            Instantiate(propMeshes[propIndex], spawners[spawnersIndex].transform.position, Quaternion.identity);
            spawners.RemoveAt(spawnersIndex);
            propMeshes.RemoveAt(propIndex);
            
            //Debug.Log("temp " + spawnersIndex);
            //Debug.Log("temp2 " + propIndex);
        }
    }

    private void Update()
    {
        if (room)
        {
            //Debug.Log("Room_PropPlacer: ");
        }
    }
}
