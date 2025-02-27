using UnityEngine;
using UnityEngine.AI;

public class FollowScript : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent enemy;
    void Start()
    {
        
    }
    void Update()
    {
        enemy.SetDestination(player.position);
    }
}
