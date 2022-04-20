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
        Debug.Log("Mouse Entered");
        ph.lookingAt = "color";
        ph.handTarget = this.gameObject;
    }

    private void OnMouseExit()
    {
        ph.lookingAt = "none";
        ph.handTarget = null;
    }
}
