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
        if (hit.CompareTag("Transition")) hit.GetComponent<Transition>().doNewRoom();
        if (hit.CompareTag("Transition2")) hit.GetComponent<Transition2>().CloseDoor();
    }
}
