using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Interactable_Socket : MonoBehaviour
{
    Room_Statue rs;
    [SerializeField] private int correctStatue;
    [HideInInspector] public GameObject assinedStatue;

    private void Awake()
    {
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

    public void onMouseEnter()
    {
        rs.ss = this.gameObject.GetComponent<Interactable_Socket>();
    }

    public void onMouseExit()
    {
        rs.ss = null;
    }
}