using UnityEngine;

public class MovingCeiling : MonoBehaviour
{
    //Is Used to move the ceiling towards the player
    public float speed;
    private void Update() { transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 7, transform.position.z), speed * Time.deltaTime); }
}
