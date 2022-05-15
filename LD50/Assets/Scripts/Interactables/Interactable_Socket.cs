using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Interactable_Socket : MonoBehaviour
{
    Room_Statue rs;
    public int correctStatue;
    public GameObject placeHolder;
    [HideInInspector] public GameObject assinedStatue;

    private void Awake()
    {
        rs = FindObjectOfType<Room_Statue>();
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