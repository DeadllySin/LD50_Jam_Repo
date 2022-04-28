using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private PlayerHand ph;
    private Color col;
    [SerializeField] private string type;
    [SerializeField] private UnityEvent onEnter;
    [SerializeField] private UnityEvent onExit;

    private void Awake()
    {
        col = GetComponent<Renderer>().material.color;
        ph = FindObjectOfType<PlayerHand>();
    }

    private void OnMouseEnter()
    {
        if (onEnter != null) onEnter.Invoke();
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
