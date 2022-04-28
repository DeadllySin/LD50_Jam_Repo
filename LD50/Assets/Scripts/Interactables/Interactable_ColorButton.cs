using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Interactable_ColorButton : MonoBehaviour
{
    public bool isPressed = true;
    private Animator anim;
    [SerializeField] private string color;
    [HideInInspector] public Room_Colors rc;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rc = FindObjectOfType<Room_Colors>();
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
}
