using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections;

public class Interactable : MonoBehaviour
{
    private PlayerHand ph;
    [SerializeField] private string type;
    private Color col;
    [SerializeField] private UnityEvent onEnter;
    [SerializeField] private UnityEvent onExit;

    private void Start()
    {
        ph = FindObjectOfType<PlayerHand>();
    }

    private void OnMouseEnter()
    {
        if(onEnter != null) onEnter.Invoke();
        GameManager.gm.lookingAtText.text = "E to Interact";
        ph.handTarget = this.gameObject;
        ph.lookingAt = type;
        col = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.gray;
    }

    private void OnMouseExit()
    {
        if (onExit != null) onExit.Invoke();
        GameManager.gm.lookingAtText.text = null;
        ph.lookingAt = "none";
        GetComponent<Renderer>().material.color = col;
        ph.handTarget = null;
    }
}
