using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Colors : MonoBehaviour
{
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private GameObject[] colors;
    private List<GameObject> spawnersList = new List<GameObject>();
    private List<GameObject> colorList = new List<GameObject>();
    private int pressedButtons;

    private void Start()
    {
        for(int i = 0; i < spawners.Length; i++)
        {
            spawnersList.Add(spawners[i]);
        }

        for(int i = 0; i < colors.Length; i++)
        {
            colorList.Add(colors[i]);
        }

        while (colorList.Count > 0) 
        {
            int temp = Random.Range(0, spawnersList.Count - 1);
            int temp2 = Random.Range(0, colorList.Count - 1);
            GameObject color = Instantiate(colorList[temp2], spawners[temp].transform.position, Quaternion.identity);
            spawners[temp] = color;
            spawnersList.RemoveAt(temp);
            colorList.RemoveAt(temp2);
        }
    }

}
