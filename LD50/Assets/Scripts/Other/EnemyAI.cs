using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    float distance;
    [SerializeField] private NavMeshAgent agent;

    void Update()
    {
        agent.SetDestination(GameManager.gm.player.transform.position);
    }
}
