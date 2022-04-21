using System.Collections.Generic;
using UnityEngine;

public class Room_Colors : MonoBehaviour
{
    [SerializeField] private Transform[] spawners;
    [SerializeField] private GameObject[] colorPrefab;
    private List<string> colorOrder = new List<string>();
    private List<GameObject> colorList = new List<GameObject>();
    private int pressedButtons;
    private bool[] correctPresses = new bool[5];
    private Room_Main rm;

    private void Awake()
    {
        rm = GetComponentInParent<Room_Main>();
        for (int i = 0; i < colorPrefab.Length; i++) colorList.Add(colorPrefab[i]);
        int j = 0;
        while (colorList.Count > 0)
        {
            int temp2 = Random.Range(0, colorList.Count - 1);
            temp2 = Random.Range(0, colorList.Count - 1);
            temp2 = Random.Range(0, colorList.Count - 1);
            temp2 = Random.Range(0, colorList.Count - 1);
            temp2 = Random.Range(0, colorList.Count - 1);
            GameObject color = Instantiate(colorList[temp2], spawners[j].position, Quaternion.identity);
            colorOrder.Add(color.GetComponent<Interactable_ColorOrder>().color);
            colorList.RemoveAt(temp2);
            j++;
        }
    }

    public void onPressed(string color)
    {
        Debug.Log("color" + color + "presed");
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pColorPress);

        if (color == colorOrder[pressedButtons])
        {
            correctPresses[pressedButtons] = true;
        }

        else
        {
            correctPresses[pressedButtons] = false;
            pressedButtons++;
        }

    }

    public void OnConfirm()
    {
        int correctPressesss = 0;
        for (int i = 0; i < correctPresses.Length; i++) if (correctPresses[i] == true) correctPressesss++;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Color_Progress", correctPressesss);
        if (correctPressesss > 2)
        {
            GameManager.gm.currTunnel.OpenDoor(0);
            if (correctPressesss == 3) rm.winState = "normal";
            if (correctPressesss == 4) rm.winState = "good";
        }
        else if(correctPressesss < 3)
        {
            GameManager.gm.currTunnel.CloseDoor(0);
            rm.winState = "bad";
        }
    }
}