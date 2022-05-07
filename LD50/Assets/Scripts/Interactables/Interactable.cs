using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private PlayerHand ph;
    private Color col;
    [SerializeField] private string type;
    [SerializeField] private UnityEvent onEnter;
    [SerializeField] private UnityEvent onExit;

    private void OnMouseEnter()
    {
        if (gameObject.GetComponent<Renderer>())
        {
            col = GetComponent<Renderer>().material.color;
            GetComponent<Renderer>().material.color = Color.gray;
        }

        ph = GameManager.gm.player.GetComponent<PlayerHand>();
        if (onEnter != null) onEnter.Invoke();
        ph.handTarget = this.gameObject;
        ph.lookingAt = type;
    }

    private void OnMouseExit()
    {
        if (onExit != null) onExit.Invoke();
        ph.lookingAt = "none";
        if (gameObject.GetComponent<Renderer>()) GetComponent<Renderer>().material.color = col;
        ph.handTarget = null;
    }
}
