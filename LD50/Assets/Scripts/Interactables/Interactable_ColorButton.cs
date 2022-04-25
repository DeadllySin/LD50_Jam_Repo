using UnityEngine;
using System.Collections;

public class Interactable_ColorButton : MonoBehaviour
{
    private PlayerHand ph;
    public bool isPressed = true;
    private Animator anim;
    [SerializeField] private string color;
    [HideInInspector] public Room_Colors rc;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rc = FindObjectOfType<Room_Colors>();
        ph = FindObjectOfType<PlayerHand>();
    }

    private void OnMouseEnter()
    {
        if(color != "confirm")
        {
            Debug.Log("Mouse Entered");
            ph.lookingAt = "color";
            ph.handTarget = this.gameObject;
        }
        else
        {
            ph.lookingAt = "confirm";
            ph.handTarget = this.gameObject;
        }
    }

    public void OnPressed()
    {
        if (!isPressed)
        {
            isPressed = true;
            anim.SetTrigger("isPressed");
            rc.OnPressed(color);
            StartCoroutine(buttonCooldown());
        }
    }

    IEnumerator buttonCooldown()
    {
        yield return new WaitForSeconds(1f);
        isPressed = false;
    }

    private void OnMouseExit()
    {
        ph.lookingAt = "none";
        ph.handTarget = null;
    }
}
