using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Cup : MonoBehaviour
{
    public Vector3 goal;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, goal,5);
    }
}
