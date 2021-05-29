using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform target; // player
    [SerializeField]
    private Transform wayPointParent;
    [SerializeField]
    private float attackDistance = 10f;
    [SerializeField]
    private float angSpeed = 5f;

    private List<Transform> wayPoints = new List<Transform>();

    private int indexCurrentWayPoint;

    enum EnemyStatus {Patrol, Chase, StopAndAttack };
    EnemyStatus currentStatus = EnemyStatus.Patrol;

    private NavMeshAgent agent;
    private MeshRenderer meshRenderer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();
        
        InitWayPoints();

        agent.SetDestination(wayPoints[indexCurrentWayPoint].position);
    }

    void Update()
    {
        UpdateStatus();
        ExecuteStatus();
    }

    void UpdateStatus()
    {
        RaycastHit hit;
        Physics.Linecast(transform.position, target.position, out hit, ~LayerMask.GetMask("Enemy"));
        Debug.DrawLine(transform.position, target.position, Color.red);     
        if(hit.collider.transform == target)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            currentStatus = distance <= attackDistance ? EnemyStatus.StopAndAttack : EnemyStatus.Chase;
        }
        else
        {
            currentStatus = EnemyStatus.Patrol;
        }

    }

    void ExecuteStatus()
    {
        switch(currentStatus)
        {
            case EnemyStatus.Patrol:
                Patrol();
                break;
            case EnemyStatus.Chase:
                Chase();
                break;
            case EnemyStatus.StopAndAttack:
                StopAndAttack();
                break;
        }
    }

    void Patrol()
    {
        agent.enabled = true;
        agent.speed = 3.5f;
        meshRenderer.sharedMaterial.color = Color.green;
        if(agent.remainingDistance < 0.05f && !agent.pathPending)
        {
            int auxRandom = Random.Range(0, wayPoints.Count);
            if (auxRandom == indexCurrentWayPoint)
                auxRandom = (indexCurrentWayPoint + 1) % wayPoints.Count;
            indexCurrentWayPoint = auxRandom;
            agent.SetDestination(wayPoints[indexCurrentWayPoint].position);
        }
    }

    void Chase()
    {
        agent.enabled = true;
        agent.speed = 7f;
        meshRenderer.sharedMaterial.color = Color.yellow;
        agent.SetDestination(target.position);
    }

    void StopAndAttack()
    {
        agent.enabled = false;
        meshRenderer.sharedMaterial.color = Color.red;

        Vector3 chaseDirection = target.position - transform.position;
        chaseDirection.y = 0;
        Quaternion rotationTarget = Quaternion.LookRotation(chaseDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, angSpeed * Time.deltaTime);
    }

    void InitWayPoints()
    {
        foreach(Transform trans in wayPointParent)
        {
            wayPoints.Add(trans);
        }

        indexCurrentWayPoint = Random.Range(0, wayPoints.Count);
    }
}
