using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Respawn")) Debug.Log(hit.gameObject.name);
        if (hit.gameObject.CompareTag("Finish")) Debug.Log(hit.gameObject.name);
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Transition")) hit.GetComponent<Transform>().GetComponentInParent<Tunnel>().NewRoom();
        if (hit.CompareTag("Transition2")) hit.GetComponent<Transform>().GetComponentInParent<Tunnel>().RemoveTunnel();
        if (hit.CompareTag("stand")) GetComponent<PlayerCrouch>().allowStandingUp = false;
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.CompareTag("stand")) GetComponent<PlayerCrouch>().allowStandingUp = true;
    }
}
