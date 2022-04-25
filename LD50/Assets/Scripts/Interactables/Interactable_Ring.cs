using UnityEngine;

public class Interactable_Ring : MonoBehaviour
{
    private PlayerHand ph;
    [HideInInspector] public Room_Ring rr;
    [HideInInspector] public Room_Ring_Main rrm;

    private void Start()
    {
        rrm = FindObjectOfType<Room_Ring_Main>();
        rr = GetComponentInParent<Room_Ring>();
        ph = FindObjectOfType<PlayerHand>();
    }

    private void OnMouseEnter()
    {
        ph.handTarget = this.gameObject;
        ph.lookingAt = "ring";
    }

    private void OnMouseExit()
    {
        ph.lookingAt = "none";
        ph.handTarget = null;
    }
}
