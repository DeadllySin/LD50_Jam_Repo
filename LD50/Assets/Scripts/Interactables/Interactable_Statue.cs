using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Interactable_Statue : MonoBehaviour
{
    private Room_Statue rs;
    public string state = "ground";
    public int statueNumber;
    public bool isHead;
    [HideInInspector] public Interactable_Socket ss;

    private void Awake()
    {
        rs = FindObjectOfType<Room_Statue>();
    }

    public void onMouseEnter()
    {
        rs.sp = this.gameObject.GetComponent<Interactable_Statue>();
    }

    public void onMouseExit()
    {
        rs.sp = null;
    }
}