using UnityEngine;

public class Interactable_Socket : MonoBehaviour
{
    PlayerHand ph;
    Room_Statue rs;
    public int correctStatue;
    [HideInInspector] public GameObject assinedStatue;

    private void Start()
    {
        ph =FindObjectOfType<PlayerHand>();
        rs = FindObjectOfType<Room_Statue>();
    }

    public void OnAssienedStatue()
    {
        if (assinedStatue != null)
        {
            if (assinedStatue.GetComponent<Interactable_Statue>().statueNumber == correctStatue)
            {
                GameManager.gm.currRoom.GetComponent<Room_Statue>().correctPieces++;
            }
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
        if(ph.handStatueTarget == this.gameObject)
        {
            ph.handStatueTarget = null;
            rs.ss = null;
        }
    }
}