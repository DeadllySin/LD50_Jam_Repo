using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Ring : MonoBehaviour
{
    private PlayerHand ph;

    private void Start()
    {
        ph = FindObjectOfType<PlayerHand>();
    }

    private void OnMouseEnter()
    {
        ph.lookingAt = "ring";
    }

    private void OnMouseExit()
    {
        ph.lookingAt = "none";
    }
}
