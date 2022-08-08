using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Memory : MonoBehaviour
{
    public MeshRenderer plate;

    public void Activate()
    {
        //if(plate.gameObject.activeInHierarchy) return;
        GetComponentInParent<Room_Memory>().RevealPair(this);
    }
}
