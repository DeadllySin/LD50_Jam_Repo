using UnityEngine;

public class StatuePiece : MonoBehaviour
{
    private PlayerHand ph;
    public string state = "ground";
    public int statueNumber;
    [HideInInspector] public StatueSocket ss;

    private void Start()
    {
        ph = FindObjectOfType<PlayerHand>();
    }

    private void OnMouseEnter()
    {
        ph.handTarget = gameObject;
        ph.sp = this.gameObject.GetComponent<StatuePiece>();
    }

    private void OnMouseExit()
    {
        if(ph.handTarget == this.gameObject)
        {
            ph.sp = null;
            ph.handTarget = null;
        }
    }
}