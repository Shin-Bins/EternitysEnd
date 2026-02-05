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

    private Animator anim;
    private AudioSource src;
    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        src = GetComponent<AudioSource>();
    }

    void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);

        if(GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            ai.speed = 0f;
            ai.SetDestination(transform.position);
            anim.SetBool("OutofSight", false);
            Debug.Log("No spooky:(");
        }
        if(!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            dest = phiast.position;
            ai.destination = dest;
            ai.speed = speed;
            anim.SetBool("OutofSight", true);
            SwitchPose();
            Debug.Log("Spooky time");
        }
    }

    void SwitchPose()
    {
        int numberGen = Random.Range(0,4);
        anim.SetInteger("Number Gen", numberGen);
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
