using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Respawn")) Debug.Log(hit.gameObject.name);
        if (hit.gameObject.CompareTag("Finish")) Debug.Log(hit.gameObject.name);
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Transition")) hit.GetComponent<Transition>().CallNewRoom();
        if (hit.CompareTag("Transition2")) hit.GetComponent<Transition2>().CloseDoor();
    }
}
