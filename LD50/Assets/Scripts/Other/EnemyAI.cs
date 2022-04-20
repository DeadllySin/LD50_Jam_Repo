using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float defaultSpeed;

    private void FixedUpdate() { transform.position = Vector3.MoveTowards(transform.position, GameManager.gm.player.transform.position, speed * Time.deltaTime); }
}
