using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Interactable_ColorButton : MonoBehaviour
{
    public string color;
    [HideInInspector] public Room_Colors rc;

    private void Awake()
    {
        rc = FindObjectOfType<Room_Colors>();
    }

    public void OnPressed()
    {
        rc.OnPressed(color);
    }
}
