using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Colors : MonoBehaviour
{
    [SerializeField] private Animator[] colorOb;
    [SerializeField] private int[] colorAmountProgress;
    private int colAmount;
    private List<string> colorOrder = new List<string>();
    private List<string> colorsPressed = new List<string>();
    private int butPressed;
    private Room_Main main;
    private int pressedCorr;
    private bool seqCooldown;

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

    IEnumerator colorOrderEnu()
    {
        for (int g = 0; g < colorOb.Length; g++)
        {
            colorOb[g].GetComponent<Interactable_ColorButton>().isPressed = true;
        }
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PressingColButtons", 0);
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pColorStart);
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
        seqCooldown = false;
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
                break;
            case "blue":
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Puzzles/Color/ColorOrderBlue", colorOb[2].gameObject);
                break;
            case "red":
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Puzzles/Color/ColorOrderRed", colorOb[1].gameObject);
                break;
            case "yellow":
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Puzzles/Color/ColorOrderYellow", colorOb[4].gameObject);
                break;
            case "orange":
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Puzzles/Color/ColorOrderOrange", colorOb[3].gameObject);
                break;      
        } 
    }

    public void Restart(Animator anim)
    {
        if (seqCooldown) return;
        anim.SetTrigger("isPressed");
        seqCooldown = true;
        StartCoroutine(colorOrderEnu());
    }

    public void OnPressed(string col)
    {
        colorsPressed.Add(col);
        butPressed += 1;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PressingColButtons", 1);
        FMOD_PlayColorOrder(col);
        Debug.Log(col);
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