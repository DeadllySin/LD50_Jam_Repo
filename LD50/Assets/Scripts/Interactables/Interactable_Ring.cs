using UnityEngine;

public class Interactable_Ring : MonoBehaviour
{
    private PlayerHand ph;
    [SerializeField] private string whichWay;
    [HideInInspector] public Room_Ring rr;

    private void Start()
    {
        rr = GetComponentInParent<Room_Ring>();
        ph = FindObjectOfType<PlayerHand>();
    }

    private void OnMouseEnter()
    {
        ph.handTarget = this.gameObject;
        if(whichWay == "down")ph.lookingAt = "ring_down";
        if(whichWay == "up")ph.lookingAt = "ring_up";
    }

    private void OnMouseExit()
    {
        ph.lookingAt = "none";
        ph.handTarget = null;
    }
}
