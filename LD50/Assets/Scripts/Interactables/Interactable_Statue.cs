using UnityEngine;

public class Interactable_Statue : MonoBehaviour
{
    private PlayerHand ph;
    private Room_Statue rs;
    public string state = "ground";
    public int statueNumber;
    [HideInInspector] public Interactable_Socket ss;

    private void Start()
    {
        rs = FindObjectOfType<Room_Statue>();
        ph = FindObjectOfType<PlayerHand>();
    }

    private void OnMouseEnter()
    {
        ph.handTarget = gameObject;
        rs.sp = this.gameObject.GetComponent<Interactable_Statue>();
    }

    private void OnMouseExit()
    {
        if(ph.handTarget == this.gameObject)
        {
            rs.sp = null;
            ph.handTarget = null;
        }
    }
}