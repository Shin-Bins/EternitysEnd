using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class EnemyPatrol : MonoBehaviour
{

    public NavMeshAgent agent;
    public float range;
    public BoxCollider vision;
    public Transform target;
    Vector3 chase;
    public Transform phist;
    public Transform skull;
   public int index;
    public float timer;
    public float chasetime;
    

    public Transform centrePoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        index = 0;
        timer = 31;
        timer += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (agent.remainingDistance <= agent.stoppingDistance && timer > 10f)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
                
            }
        }

        if (index == 1)
        {
            chase = phist.position;
            agent.SetDestination(chase);
            
        }

        if (index == 2)
        {
            chase = skull.position;
            agent.SetDestination(chase);
        }

        if (timer > 10f)
        {
            index = 3;
        }

        if (index == 3)
        {
            Vector3 point;
           if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);

            }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "phiast")
        {
            index = 1;
            timer = 0f;

            
            

           
        }

        if (other.gameObject.tag == "skull")
        {
            index = 2;
            timer = 0f;

            


        }
    }
}
