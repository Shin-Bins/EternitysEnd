using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class WeepingAngel : MonoBehaviour
{
    private NavMeshAgent ai;
    public Transform phiast;
    Vector3 dest;
    public float speed, damageDist;
    public Camera cam;

    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);

        if(GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            ai.speed = 0f;
            ai.SetDestination(transform.position);
            Debug.Log("No spooky:(");
        }
        if(!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            dest = phiast.position;
            ai.destination = dest;
            ai.speed = speed;
            Debug.Log("Spooky time");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("phiast"))
        {
            PhiastHealth health = other.GetComponent<PhiastHealth>();
            if(health != null)
            {
                health.TakeDamage(transform.position);
            }
        }
    }
}
