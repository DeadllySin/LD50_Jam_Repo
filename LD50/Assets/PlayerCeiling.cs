using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCeiling : MonoBehaviour
{
    [HideInInspector] public bool isCol;
    private void OnCollisionEnter(Collision collision)
    {
        isCol = true;
        Debug.Log("test");
    }

    private void OnCollisionExit(Collision collision)
    {
        isCol = false;
        Debug.Log("test2");
    }
}
