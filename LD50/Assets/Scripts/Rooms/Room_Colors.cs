using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Colors : MonoBehaviour
{
    [SerializeField] private Transform[] spawners;
    [SerializeField] private GameObject[] colorPrefab;
    private List <string> colorOrder = new List<string>();
    private List<GameObject> colorList = new List<GameObject>();
    private int pressedButtons;
    private bool[] correctPresses = new bool[5];

    private void Start()
    {
        for(int i = 0; i < colorPrefab.Length; i++)
        {
            colorList.Add(colorPrefab[i]);
        }

        int j = 0;
        while (colorList.Count > 0) 
        {

            int temp2 = Random.Range(0, colorList.Count - 1);
            GameObject color = Instantiate(colorList[temp2], spawners[j].position, Quaternion.identity);
            colorOrder.Add(color.GetComponent<Interactable_ColorOrder>().color);
            colorList.RemoveAt(temp2);
            j++;
        }
    }

    public void onPressed(string color)
    {
        Debug.Log("color" + color + "presed");
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
