using System.Collections.Generic;
using UnityEngine;

public class Room_PropPlacer : MonoBehaviour
{
    readonly List<GameObject> spawners = new List<GameObject>();
    readonly List<GameObject> propMeshes = new List<GameObject>();
    [SerializeField] private GameObject[] propMeshess;
    [SerializeField] private int spawnCount = 0;

    private void Awake()
    {
        for (int i = 0; i < propMeshess.Length; i++) propMeshes.Add(propMeshess[i]);
        foreach (Transform child in gameObject.transform) spawners.Add(child.gameObject);
        if (spawnCount < propMeshes.Count)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                int spawnersIndex = Random.Range(0, spawners.Count);
                int propIndex = Random.Range(0, propMeshes.Count);
                GameObject prop = Instantiate(propMeshes[propIndex], spawners[spawnersIndex].transform.position, Quaternion.identity);
                prop.transform.parent = gameObject.GetComponentInParent<Transform>();
                spawners.RemoveAt(spawnersIndex);
                propMeshes.RemoveAt(propIndex);
            }
        }
        else
        {
            for (int i = 0; i < spawnCount; i++)
            {
                int spawnersIndex = Random.Range(0, spawners.Count);
                int propIndex = Random.Range(0, propMeshes.Count);
                GameObject prop = Instantiate(propMeshes[propIndex], spawners[spawnersIndex].transform.position, Quaternion.identity);
                prop.transform.parent = gameObject.GetComponentInParent<Transform>();
                spawners.RemoveAt(spawnersIndex);
            }
        }
    }
}
