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

    private void Awake()
    {
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
    }

    public void OnConfirm()
    {
        for (int i = 0; i < colorOrder.Count; i++)
        {
            if (colorOrder[i] == colorsPressed[i]) buttonsPressedCorrectly += 1;
        }
        Debug.Log("correctPressed = " + buttonsPressedCorrectly);
    }
}