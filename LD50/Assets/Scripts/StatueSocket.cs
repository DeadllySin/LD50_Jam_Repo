using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueSocket : MonoBehaviour
{
    PlayerHand ph;
    public int correctStatue;
    public GameObject assinedStatue;

    private void Start()
    {
        ph =FindObjectOfType<PlayerHand>(); 
    }

    private void OnMouseEnter()
    {
        ph.handStatueTarget = gameObject;
    }
}
