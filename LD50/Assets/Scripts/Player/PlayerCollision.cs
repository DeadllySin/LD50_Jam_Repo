using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Respawn") Debug.Log(hit.gameObject.name);
        if (hit.gameObject.tag == "Finish") Debug.Log(hit.gameObject.name);
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Transition")
        {
            Debug.Log(hit.gameObject.name);
        }
    }
}
