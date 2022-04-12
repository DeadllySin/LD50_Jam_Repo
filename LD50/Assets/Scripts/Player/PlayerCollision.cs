using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject nextRoom;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Respawn") Debug.Log(hit.gameObject.name);
        if (hit.gameObject.tag == "Finish") Debug.Log(hit.gameObject.name);
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Transition")
        {
            StartCoroutine(hit.GetComponent<Transition>().newRoom());
        }
        if (hit.gameObject.tag == "Transition2")
        {
            hit.GetComponent<Transition2>().CloseDoor();
        }
    }
}
