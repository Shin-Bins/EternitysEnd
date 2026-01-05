using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class EnemyPatrol : MonoBehaviour
{

    private NavMeshAgent agent;
    public float range;
    public Transform target;
    Vector3 chase;
    public Transform phist;
    public Transform skull;
    public int index;
    public float timer;
    public float chasetime;
    float radius = 5;

    public Transform centrePoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        index = 0;
       // timer = 7f;         
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        TargetCheck();
        StyleSwitching();
    }

    void StyleSwitching()
    {
        switch (index)
        {
            case 0:
                RoyalGuard();
                break;
            case 1:
                Chase(phist);
                break;
            case 2:
                Chase(skull);
                break;
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;

    }

    void RoyalGuard()
    {
         if (index == 0)
        {
            if (agent.remainingDistance <= agent.stoppingDistance && timer > 5f)
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    agent.SetDestination(point);
                    timer = 0f;
                }
            }
        }
    }

    void TargetCheck()
    {
        if (skull != null && Vector3.Distance(centrePoint.position, skull.position) <= range)
        {
            index = 2;
            timer = 0f;
        }
        else if (phist != null && Vector3.Distance(centrePoint.position, phist.position) <= range)
        {
            index = 1;
            timer = 0f;
        }

        else if (timer > 10f && (index == 1 || index == 2))
        {
            index = 0;
            timer = 0f;
        }
    }

    void Chase(Transform target)
    {
        if (target != null)
        {
            chase = ClampPosition(target.position);
            agent.SetDestination(chase);
        }
    }

    Vector3 ClampPosition(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - centrePoint.position;
    
        if (directionToTarget.magnitude > range)
        {
            directionToTarget = directionToTarget.normalized * range;
            return centrePoint.position + directionToTarget;
        }

        return targetPosition;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(centrePoint.position, range);
    }

    void OnEnable()
    {
     // reset everything to avoid ai freaking out if set inactive and active
        index = 0;
        timer = 0f;

        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (agent != null && agent.isOnNavMesh)
        {
            agent.ResetPath();
            agent.isStopped = false;
        
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                agent.SetDestination(point);
            }
        }
    }
    void OnDisable()
    {
        if (agent != null && agent.isOnNavMesh)
        {
            agent.ResetPath();
            agent.isStopped = true;
        }
    }
}
