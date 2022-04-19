using UnityEngine;

public class Interactable_Ring : MonoBehaviour
{
    private PlayerHand ph;
    [SerializeField] private string whichWay;
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
        if(whichWay == "down")ph.lookingAt = "ring_down";
        if(whichWay == "up")ph.lookingAt = "ring_up";
        if (whichWay == "confirm") ph.lookingAt = "confirm";
    }

    private void OnMouseExit()
    {
        ph.lookingAt = "none";
        ph.handTarget = null;
    }
}
