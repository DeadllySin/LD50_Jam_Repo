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
            case 4:
                colAmount = colorAmountProgress[2];
                break;
            case 5:
                colAmount = colorAmountProgress[2];
                break;

        }
        GameManager.gm.colorRoomPro++;
        main = GetComponentInParent<Room_Main>();
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
            colorOb[rdm].SetTrigger("isPressed");
            yield return new WaitForSeconds(1f);
            i += 1;
        }
        for (i = 0; i < colorOb.Length; i++)
        {
            colorOb[i].GetComponent<Interactable_ColorButton>().isPressed = false;
        }
    }

    public void Restart()
    {
        for(int i = 0; i < colorOb.Length; i++)
        {
            colorOb[i].GetComponent<Interactable_ColorButton>().isPressed = true;
        }
        StartCoroutine(colorOrderEnu());
    }

    public void OnPressed(string col)
    {
        colorsPressed.Add(col);
        butPressed += 1;
        if (butPressed == colAmount)
        {
            OnValueChanged();
        }

    }

    public void OnValueChanged()
    {
        for (int i = 0; i < colorOrder.Count; i++)
        {
            if(colorOrder[i] == colorsPressed[i])
            {
                pressedCorr++;
            }
        }
        if (pressedCorr == colorOrder.Count)
        {
            main.state = "perfect";
        }
        if (pressedCorr == colorOrder.Count - 1)
        {
            main.state = "ok";
        }
        else if (colorsPressed.Count > colorOrder.Count)
        {
            main.state = "bad";
        }
    }
}