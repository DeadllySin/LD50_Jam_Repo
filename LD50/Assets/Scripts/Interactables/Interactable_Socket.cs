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
        ph.handStatueTarget = gameObject;
        rs.ss = this.gameObject.GetComponent<Interactable_Socket>();
    }

    private void OnMouseOver()
    {
        ph.handStatueTarget = gameObject;
        rs.ss = this.gameObject.GetComponent<Interactable_Socket>();
    }

    private void OnMouseExit()
    {
        if (ph.handStatueTarget == this.gameObject)
        {
            ph.handStatueTarget = null;
            rs.ss = null;
        }
    }
}