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

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Respawn")
        {
            Debug.Log(hit.gameObject.name);
            ceiling c = hit.gameObject.GetComponent<ceiling>();
            c.speed = 0;
            Debug.Log("You Are DEAD");
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Respawn")
        {
            Debug.Log(hit.gameObject.name);
            ceiling c = hit.gameObject.GetComponent<ceiling>();
            c.speed = 0;
            Debug.Log("You Are DEAD");
        }
        if (hit.gameObject.tag == "Finish")
        {
            Debug.Log(hit.gameObject.name);
            GameManager gm = FindObjectOfType<GameManager>();
            gm.SpawnRoom(hit.gameObject);
        }
    }
}
