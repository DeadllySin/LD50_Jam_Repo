using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuePiece : MonoBehaviour
{
    public string state = "ground";
    private PlayerHand hand;
    public int statueNumber;

    private void Start()
    {
        hand = FindObjectOfType<PlayerHand>();
    }

    private void OnMouseEnter()
    {
        hand.handTarget = gameObject;
    }
}
