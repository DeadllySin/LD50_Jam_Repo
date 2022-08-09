using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Interactable_Cup : MonoBehaviour
{
    public Vector3 goal;
    [HideInInspector] public bool canInteract;
    [HideInInspector] public bool isUp;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!canInteract && goal != Vector3.zero) transform.position = Vector3.MoveTowards(transform.position, goal,.05f);
    }
}
