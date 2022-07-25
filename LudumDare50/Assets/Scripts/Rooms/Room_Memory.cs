using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Memory : MonoBehaviour
{
    [SerializeField] private Interactable_Memory[] plates;
    [SerializeField] private List<Material> symbols;
    private Interactable_Memory[] mem = new Interactable_Memory[2];
    Room_Main rm;
    int howMuchActive = 0;

    private void Start()
    {
        rm = GetComponentInParent<Room_Main>();
        for (int i = 0; i < plates.Length; i++)
        {
            int rdm = Random.Range(0, symbols.Count);
            plates[i].plate.material = symbols[rdm];
            symbols.RemoveAt(rdm);
        }
    }

    public void RevealAllPlates()
    {
        StartCoroutine(revealEnu());
    }

    public void RevealPair(Interactable_Memory im)
    {
        im.plate.gameObject.SetActive(true);
        if (mem[0] == null) mem[0].plate = im.plate;
        else
        {
            mem[1].plate = im.plate;
            StartCoroutine(revealPairEnu());
        }

    }

    IEnumerator revealPairEnu()
    {
        yield return new WaitForSeconds(1);
        if (mem[0].plate.name == mem[1].plate.name)
        {
            mem[0].hasFoundPair = true;
            mem[1].hasFoundPair = true;
            howMuchActive++;
            if (howMuchActive == 4)
            {
                rm.state = "perfect";
            }
            else if (howMuchActive == 3) rm.state = "ok";
        }
        else
        {
            mem[0].plate.gameObject.SetActive(false);
            mem[1].plate.gameObject.SetActive(false);
            mem[0] = null;
            mem[1] = null;

            for (int i = 0; i < plates.Length; i++)
            {
                if (plates[i].plate.gameObject.activeInHierarchy) howMuchActive++;
            }
        }
    }

    IEnumerator revealEnu()
    {
        for (int i = 0; i < plates.Length; i++)
        {
            plates[i].plate.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        for (int i = 0; i < plates.Length; i++)
        {
            plates[i].plate.gameObject.SetActive(false);
        }
    }
}
