using UnityEngine;

public class StatueSocket : MonoBehaviour
{
    PlayerHand ph;
    public int correctStatue;
    public GameObject assinedStatue;

    private void Start()
    {
        ph =FindObjectOfType<PlayerHand>(); 
    }

    public void OnAssienedStatue()
    {
        if (assinedStatue != null) if (assinedStatue.GetComponent<StatuePiece>().statueNumber == correctStatue) GameManager.gm.currRoom.GetComponent<StatueRoomManager>().correctPieces++;
    }

    private void OnMouseEnter()
    {
        ph.handStatueTarget = gameObject;
        ph.ss = this.gameObject.GetComponent<StatueSocket>();
    }

    private void OnMouseOver()
    {
        ph.handStatueTarget = gameObject;
        ph.ss = this.gameObject.GetComponent<StatueSocket>();
    }

    private void OnMouseExit()
    {
        if(ph.handStatueTarget == this.gameObject)
        {
            ph.handStatueTarget = null;
            ph.ss = null;
        }
    }
}