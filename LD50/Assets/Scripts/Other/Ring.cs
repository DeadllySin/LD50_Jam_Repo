using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private PlayerHand ph;

    private void Awake()
    {
        ph = FindObjectOfType<PlayerHand>();
    }

    private void OnMouseEnter()
    {
        ph.handTarget = this.gameObject;
    }
}
