using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition2 : MonoBehaviour
{
    [SerializeField] private Animator doorAnim;
    public void CloseDoor()
    {
        doorAnim.SetTrigger("isClosed");
    }
}
