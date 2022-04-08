using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Respawn")
        {
            Debug.Log(hit.gameObject.name);
        }
        if (hit.gameObject.tag == "Finish")
        {
            Debug.Log(hit.gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Trigger1")
        {
            Debug.Log(hit.gameObject.name);
            GameManager gm = FindObjectOfType<GameManager>();
            gm.SpawnRoom(hit.gameObject);
        }
    }
}
