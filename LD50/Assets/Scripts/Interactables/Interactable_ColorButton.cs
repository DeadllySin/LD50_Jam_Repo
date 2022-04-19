using UnityEngine;

public class Interactable_ColorButton : MonoBehaviour
{
    private PlayerHand ph;
    [HideInInspector] public Room_Colors rc;
    public string color;

    private void Start()
    {
        rc = FindObjectOfType<Room_Colors>();
        ph = FindObjectOfType<PlayerHand>();
    }

    private void OnMouseEnter()
    {
        ph.lookingAt = "color";
        ph.handTarget = gameObject;
    }

    private void OnMouseExit()
    {
        ph.lookingAt = "none";
        ph.handTarget = null;
    }
}
