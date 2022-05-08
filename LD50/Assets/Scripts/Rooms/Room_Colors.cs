using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Colors : MonoBehaviour
{
    public Animator[] colorOb;
    [SerializeField] private int[] colorAmountProgress;
    private int colAmount;
    private List<string> colorOrder = new List<string>();
    private List<string> colorsPressed = new List<string>();
    private int butPressed;
    private Room_Main main;
    private int pressedCorr;

    FMOD.Studio.EventInstance colorPressInstance;

    private void Awake()
    {
        switch (GameManager.gm.colorRoomPro)
        {
            case 0:
                colAmount = colorAmountProgress[0];
                break;
            case 1:
                colAmount = colorAmountProgress[0];
                break;
            case 2:
                colAmount = colorAmountProgress[1];
                break;
            case 3:
                colAmount = colorAmountProgress[1];
                break;
            default:
                colAmount = colorAmountProgress[2];
                break;

        }
        GameManager.gm.colorRoomPro++;
        main = GetComponentInParent<Room_Main>();
    }

    private void Start()
    {
        colorPressInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.pColorPress);
    }

    IEnumerator colorOrderEnu()
    {
        colorOrder.Clear();
        colorsPressed.Clear();
        pressedCorr = 0;
        butPressed = 0;
        yield return new WaitForSeconds(1.5f);
        int i = 0;
        while (i < colAmount)
        {
            int rdm = Random.Range(0, colorOb.Length);
            rdm = Random.Range(0, colorOb.Length);
            rdm = Random.Range(0, colorOb.Length);
            colorOrder.Add(colorOb[rdm].GetComponent<Interactable_ColorButton>().color);
            FMOD_PlayColorOrder(colorOb[rdm].GetComponent<Interactable_ColorButton>().color);
            colorOb[rdm].SetTrigger("isPressed");
            yield return new WaitForSeconds(1f);
            i += 1;
        }
        for (i = 0; i < colorOb.Length; i++)
        {
            colorOb[i].GetComponent<Interactable_ColorButton>().isPressed = false;
        }
    }

    public void FMOD_PlayColorOrder(string col)
    {
        switch (col)
        {
            default:
                Debug.Log("Error");
                break;
            case "green":
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Puzzles/Color/ColorOrderGreen", colorOb[0].gameObject);
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Color_Order_Pitch", "Green");
                break;
            case "blue":
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Puzzles/Color/ColorOrderBlue", colorOb[2].gameObject);
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Color_Order_Pitch", "Blue");
                break;
            case "red":
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Puzzles/Color/ColorOrderRed", colorOb[1].gameObject);
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Color_Order_Pitch", "Red");
                break;
            case "yellow":
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Puzzles/Color/ColorOrderYellow", colorOb[4].gameObject);
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Color_Order_Pitch", "Yellow");
                break;
            case "orange":
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Puzzles/Color/ColorOrderOrange", colorOb[3].gameObject);
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Color_Order_Pitch", "Orange");
                break;      
        } 
    }


    public void Restart()
    {
        for (int i = 0; i < colorOb.Length; i++)
        {
            colorOb[i].GetComponent<Interactable_ColorButton>().isPressed = true;
        }
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pColorStart);
        StartCoroutine(colorOrderEnu());
    }

    public void OnPressed(string col)
    {
        colorsPressed.Add(col);
        butPressed += 1;
        colorPressInstance.start();
        FMOD_PlayColorOrder(col);
        //FMOD_PlayColorOrder(GetComponent<Interactable_ColorButton>().color);
        if (butPressed == colAmount) OnValueChanged();
    }

    public void OnValueChanged()
    {
        for (int i = 0; i < colorOrder.Count; i++)
        {
            if (colorOrder[i] == colorsPressed[i]) pressedCorr++;
        }
        if (pressedCorr == colorOrder.Count) main.state = "perfect";
        else if (pressedCorr == colorOrder.Count - 1) main.state = "ok";
        else if (colorsPressed.Count > colorOrder.Count) main.state = "bad";
    }
}