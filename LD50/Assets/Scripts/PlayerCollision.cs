using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Respawn")
        {
            Debug.Log(hit.gameObject.name);
        }
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Respawn")
        {
            Debug.Log(hit.gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Respawn")
        {
            Debug.Log(hit.gameObject.name);
            ceiling c = hit.gameObject.GetComponent<ceiling>();
            c.speed = 0;
        }
    }
}
