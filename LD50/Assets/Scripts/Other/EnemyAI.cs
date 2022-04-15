using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,GameManager.gm.player.transform.position, 1 * Time.deltaTime);
    }
}
