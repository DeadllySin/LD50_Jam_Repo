using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Memory : MonoBehaviour
{
    [HideInInspector] public MeshRenderer plate;
    [HideInInspector] public bool hasFoundPair;

    public void Activate()
    {
        if(hasFoundPair) return;
        GetComponentInParent<Room_Memory>().RevealPair(this);
    }
}
