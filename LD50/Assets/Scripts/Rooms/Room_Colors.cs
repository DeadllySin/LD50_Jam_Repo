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
        if (butPressed == amountOfRandomColors)
        {
            OnValueChanged();
        }

    }

    public void OnValueChanged()
    {
        Debug.Log(1);
        for (int i = 0; i < colorOrder.Count; i++)
        {
            main.state = "ok";
        }
        Debug.Log(2);
        if (colorsPressed.Count == colorOrder.Count)
        {
            main.state = "perfect";
        }
        else if (colorsPressed.Count > colorOrder.Count)
        {
            main.state = "bad";
        }
        Debug.Log(3);
    }
}