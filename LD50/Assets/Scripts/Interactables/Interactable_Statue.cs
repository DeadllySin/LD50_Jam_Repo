using UnityEngine;

public class Interactable_Statue : MonoBehaviour
{
    private PlayerHand ph;
    private Room_Statue rs;
    public string state = "ground";
    public int statueNumber;
    [HideInInspector] public Interactable_Socket ss;
    private Material mat;

    private void Start()
    {
        rs = FindObjectOfType<Room_Statue>();
        ph = FindObjectOfType<PlayerHand>();
    }

    private void OnMouseEnter()
    {
        ph.lookingAt = "statue";
        ph.handTarget = gameObject;
        rs.sp = this.gameObject.GetComponent<Interactable_Statue>();
        //mat = GetComponent<Renderer>().material;
        //GetComponent<Renderer>().material = GameManager.gm.highlightMat;
        GetComponent<Renderer>().material.color = Color.gray;
    }

    private void OnMouseExit()
    {
        if (ph.handTarget == this.gameObject)
        {
            //GetComponent<Renderer>().material = mat;
            GetComponent<Renderer>().material.color = Color.white;
            ph.lookingAt = "none";
            rs.sp = null;
            ph.handTarget = null;
        }
    }
}