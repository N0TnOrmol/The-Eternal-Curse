using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LungeTest : MonoBehaviour
{
    public float LungeDistance = 5f;
    public float LungeSpeed = 20f;
    public float LungeCoolDown = 2f;
    private NavMeshAgent Agent;
    private Vector3 TargetPosition;
    private bool IsLunging = false;
    private bool OnCoolDown = false;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(IsLunging || OnCoolDown) return;
        if(Vector3.Distance(transform.position, Player.instance.transform.position) <= LungeDistance)
        {
            StartCoroutine(LungeAtPlayer());
        }
    }

    IEnumerator LungeAtPlayer()
    {
        IsLunging = true;
        Agent.isStopped = true;

        TargetPosition = Player.instance.transform.position;
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        while(elapsedTime < 0.2f)
        {
            transform.position = Vector3.Lerp(startPosition, TargetPosition, (elapsedTime / 0.2f));
            elapsedTime += Time.deltaTime * LungeSpeed;
            yield return null;
        }
        transform.position = TargetPosition;
        yield return new WaitForSeconds(1f);
        IsLunging = false;
        OnCoolDown = false;
        Agent.isStopped = false;
        yield return new WaitForSeconds(LungeCoolDown);
        OnCoolDown = false;
    }
}
