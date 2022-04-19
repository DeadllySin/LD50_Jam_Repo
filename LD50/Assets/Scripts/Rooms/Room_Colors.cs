using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Colors : MonoBehaviour
{
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private GameObject[] colorPrefab;
    private List <string> colorOrder;
    private List<GameObject> spawnersList = new List<GameObject>();
    private List<GameObject> colorList = new List<GameObject>();
    private int pressedButtons;
    private bool[] correctPresses = new bool[5];

    private void Start()
    {
        for(int i = 0; i < spawners.Length; i++)
        {
            spawnersList.Add(spawners[i]);
        }

        for(int i = 0; i < colorPrefab.Length; i++)
        {
            colorList.Add(colorPrefab[i]);
        }

        int j = 0;
        while (colorList.Count > 0) 
        {

            int temp = Random.Range(0, spawnersList.Count - 1);
            int temp2 = Random.Range(0, colorList.Count - 1);
            GameObject color = Instantiate(colorList[temp2], spawners[temp].transform.position, Quaternion.identity);
            spawners[temp] = color;
            colorOrder.Add(color.GetComponent<Interactable_ColorOrder>().color);
            spawnersList.RemoveAt(temp);
            colorList.RemoveAt(temp2);
            j++;
        }
    }

    public void onPressed(string color)
    {
        if(color == colorOrder[pressedButtons])
        {
            correctPresses[pressedButtons] = true;
        }else
        {
            correctPresses[pressedButtons] = false;
        }
        pressedButtons++;
    }

}
