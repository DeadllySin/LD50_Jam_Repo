using UnityEngine;

public class Interactable_Ring : MonoBehaviour
{
    private PlayerHand ph;
    [SerializeField] private string whichWay;

    private void Start()
    {
        ph = FindObjectOfType<PlayerHand>();
    }

    private void OnMouseEnter()
    {
        if(whichWay == "down")ph.lookingAt = "ring_down";
        if(whichWay == "up")ph.lookingAt = "ring_up";
    }

    private void OnMouseExit()
    {
        ph.lookingAt = "none";
    }
}
