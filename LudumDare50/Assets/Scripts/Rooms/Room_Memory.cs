using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Memory : MonoBehaviour
{
    private List<Interactable_Memory> plates = new List<Interactable_Memory>();
    [SerializeField] private List<Material> symbols;
    private List<MeshRenderer> opened = new List<MeshRenderer>();
    Room_Main rm;
    int howMuchActive = 0;
    [SerializeField] private float[] revealTimeAll;
    float revealTime;

    private void Start()
    {
        foreach (Transform child in this.transform)
        {
            if (child.GetComponent<Interactable_Memory>()) plates.Add(child.GetComponent<Interactable_Memory>());
        }

        rm = GetComponentInParent<Room_Main>();
        for (int i = 0; i < plates.Count; i++)
        {
            Debug.Log("symbols lenght: " + symbols.Count + "plates lenght" + plates.Count);
            int rdm = Random.Range(0, symbols.Count);
            plates[i].plate.material = symbols[rdm];
            symbols.RemoveAt(rdm);
        }

        revealTime = revealTimeAll[rm.gm.memoryRoomPro];
        rm.gm.memoryRoomPro++;
        rm.gm.memoryRoomPro = Mathf.Clamp(rm.gm.memoryRoomPro, 0, 4);
    }

    private void FixedUpdate()
    {
        Debug.Log(howMuchActive);
    }

    public void RevealAllPlates()
    {
        StartCoroutine(revealEnu());
    }

    public void RevealPair(Interactable_Memory im)
    {
        im.plate.gameObject.SetActive(true);
        if (opened.Count == 0)
        {
            opened.Add(im.plate);
        }
        else
        {
            opened.Add(im.plate);
            StartCoroutine(revealPairEnu());
        }
    }

    IEnumerator revealPairEnu()
    {
        yield return new WaitForSeconds(1);
        if (opened[0].material.name == opened[1].material.name)
        {
            Debug.LogError("Sound for when both colors match");
            howMuchActive += 1;
            if (howMuchActive == 4)
            {
                rm.state = "perfect";
            }
            else if (howMuchActive == 3) rm.state = "ok";
            opened.Clear();
        }
        else
        {
            Debug.LogError("Sound for when both colors DON'T match");
            opened[0].gameObject.SetActive(false);
            opened[1].gameObject.SetActive(false);
            opened.Clear();
        }
    }

    IEnumerator revealEnu()
    {
        Debug.Log(plates.Count);
        for (int i = 0; i < plates.Count; i++)
        {
            plates[i].plate.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(revealTime);
        for (int i = 0; i < plates.Count; i++)
        {
            plates[i].plate.gameObject.SetActive(false);
        }
    }
}
