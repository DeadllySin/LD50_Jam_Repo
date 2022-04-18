using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    void FixedUpdate() { agent.SetDestination(GameManager.gm.player.transform.position); }
}
