using UnityEngine;

public class StatuePiece : MonoBehaviour
{
    public string state = "ground";
    private PlayerHand ph;
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