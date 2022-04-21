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
    }

    private void Start()
    {
        while (propMeshes.Count > 0)
        {
            int temp = Random.Range(0, spawners.Count - 1);
            int temp2 = Random.Range(0, propMeshes.Count - 1);
            Instantiate(propMeshes[temp2], spawners[temp].transform.position, Quaternion.identity);
            spawners.RemoveAt(temp);
            propMeshes.RemoveAt(temp2);
        }
    }

    private void Update()
    {
        if (room)
        {
            Debug.Log("Room_PropPlacer: ");
        }
    }
}
