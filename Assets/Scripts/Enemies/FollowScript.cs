using UnityEngine;
using UnityEngine.AI;

public class FollowScript : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent enemy;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
    }
    void Update()
    {
        enemy.SetDestination(player.position);
    }
}
