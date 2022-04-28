using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Colors : MonoBehaviour
{
    public Animator[] colorOb;
    [SerializeField] private string[] colorArray;
    [SerializeField] private float amountOfRandomColors;
    private List<string> colorOrder = new List<string>();
    private List<string> colorsPressed = new List<string>();
    private int buttonsPressedCorrectly;
    private int butPressed;
    private Room_Main main;

    private void Awake()
    {
        main = GetComponentInParent<Room_Main>();
        StartCoroutine(colorOrderEnu());
    }

    IEnumerator colorOrderEnu()
    {
        yield return new WaitForSeconds(2f);
        int i = 0;
        while (i < amountOfRandomColors)
        {
            int rdm = Random.Range(0, colorArray.Length);
            rdm = Random.Range(0, colorArray.Length);
            rdm = Random.Range(0, colorArray.Length);
            colorOrder.Add(colorArray[rdm]);
            colorOb[rdm].SetTrigger("isPressed");
            yield return new WaitForSeconds(1f);
            i += 1;
        }
        for (i = 0; i < colorOb.Length; i++)
        {
            colorOb[i].GetComponent<Interactable_ColorButton>().isPressed = false;
        }
    }

    public void OnPressed(string col)
    {
        colorsPressed.Add(col);
        butPressed += 1;
        if(butPressed == amountOfRandomColors)
        {
            OnValueChanged();
        }

    }

    public void OnValueChanged()
    {
        Debug.Log(1);
        buttonsPressedCorrectly = 0;
        for (int i = 0; i < colorOrder.Count; i++)
        {
            Debug.Log("I" + i);
            if (colorOrder[i] == colorsPressed[i]) buttonsPressedCorrectly += 1;
        }
        Debug.Log(2);
        if (colorsPressed.Count == colorOrder.Count)
        {
            Debug.Log(2.2);
            if (colorsPressed.Count == buttonsPressedCorrectly)
            {
                Debug.Log(2.3);
                GameManager.gm.currTunnel.OpenDoor(0);
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleCorrect);
            }
        }
        else if(colorsPressed.Count > colorOrder.Count)
        {
            Debug.Log(2.9);
            GameManager.gm.currTunnel.CloseDoor(0);
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleWrong);
        }
        Debug.Log(3);
    }
}