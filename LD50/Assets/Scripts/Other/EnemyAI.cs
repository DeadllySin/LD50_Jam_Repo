using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float defaultSpeed;
    void FixedUpdate() { transform.position = Vector3.MoveTowards(transform.position, GameManager.gm.player.transform.position, speed * Time.deltaTime); }
}
