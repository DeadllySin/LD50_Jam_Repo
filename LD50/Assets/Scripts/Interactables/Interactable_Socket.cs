using UnityEngine;

public class Interactable_Socket : MonoBehaviour
{
    PlayerHand ph;
    Room_Statue rs;
    [SerializeField] private int correctStatue;
    [HideInInspector] public GameObject assinedStatue;

    private void Start()
    {
        ph = FindObjectOfType<PlayerHand>();
        rs = FindObjectOfType<Room_Statue>();
    }

    public void OnAssienedStatue()
    {
        if (assinedStatue.GetComponent<Interactable_Statue>().statueNumber == correctStatue) rs.correctPieces++;
    }

    public void OnRemovedStatue()
    {
        if (assinedStatue.GetComponent<Interactable_Statue>().statueNumber == correctStatue)
        {
            rs.correctPieces--;
        }
    }

    private void OnMouseEnter()
    {
        ph.handTarget = gameObject;
        ph.lookingAt = "socket";
        rs.ss = this.gameObject.GetComponent<Interactable_Socket>();
    }

    private void OnMouseOver()
    {
        ph.handTarget = gameObject;
        rs.ss = this.gameObject.GetComponent<Interactable_Socket>();
    }

    private void OnMouseExit()
    {
        if (ph.handTarget == this.gameObject)
        {
            ph.handTarget = null;
            rs.ss = null;
            ph.lookingAt = null;
        }
    }
}