using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol")]
    private NavMeshAgent agent;
    public Transform centrePoint;
    public float range;
    private float timer;
    public Transform target;
    float radius = 5f;

    [Header("Chase")]
    Vector3 chase;
    public Transform phist;
    public Transform skull;
    private float chasetime;

    [Header("Attack")]
    [SerializeField]private bool isAttacking = false;
    [SerializeField]private float attackRange = 3f;
    [SerializeField] private float attackTimer;
    [SerializeField] private float attackCooldown = 5f;
    public GameObject damageZone;
    

    [Header("Grab Cuan")]
    public bool isHolding = false;
    private float skullKillTimer = 3f;
    private float grabRange = 2f;
    private Rigidbody cuanRb;//this is a reference for when we pick up cuan
	private BoxCollider cuanColl;//turn off collision with skull when carried. Was having some funky effects on phiast
	public Transform cuanPosition;//this is where cuan is held
    private CuanHealth skullyHealth;

    [Header("Stnned")]
    private bool isStunned = false;
    private float stunTime = 3f;
    private float stunTimer = 0f;

    public int index;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        damageZone.SetActive(false);
        agent.updateRotation = true; 
        index = 0;
        timer = 7f;  
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }

        if(isStunned)
        {
            stunTimer -= Time.deltaTime;
            if(stunTimer <= 0f)
            {
                isStunned = false;
            }
            return;
        }

        StateCheck();
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
            case 3:
                SwordMaster();
                break;
            case 4:
                SkullMaster();
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

    void StateCheck()//position of the index entry matters. The higher it is the higher the priority
    {

        float distToPhist = Vector3.Distance(transform.position, phist.position);
        float distToCuan = Vector3.Distance(transform.position, skull.position);

        if(distToCuan <= grabRange && !isHolding && CharacterManager.Instance.cuanHeld == false)
        {
            index = 4;
            return;
        }

        // Chase conditions
        if (skull != null && Vector3.Distance(centrePoint.position, skull.position) <= range)
        {
            index = 2;
            timer = 0f;
        }
        if (distToPhist <= attackRange && !isHolding)
        {
            index = 3;
            return;
        }
        else if (Vector3.Distance(centrePoint.position, phist.position) <= range)
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

    void RoyalGuard()
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

    void Chase(Transform target)
    {
        if (target != null)
        {
            agent.isStopped = false;
            chase = ClampPosition(target.position);
            agent.SetDestination(chase);
        }
    }

    public bool CanAttack()
    {
       return !isAttacking && attackTimer <= 0f;
    }

    void SwordMaster()
    {
        if(!CanAttack()) return;
        isAttacking = true;
        agent.isStopped = true;
        agent.updateRotation = false;
        transform.LookAt(target);
        damageZone.SetActive(true);
        
        Invoke("EndAttack", 0.1f);//This is to test out attacking enemies. A better way would be to use a collider that is activated mid animation. Until we have animations, this is the way
    }

    void EndAttack()
    {
        damageZone.SetActive(false);
        isAttacking = false;
        agent.isStopped = false;
        agent.updateRotation = true; 
        attackTimer = attackCooldown;
        StateCheck();
    }

    void SkullMaster()
    {
        if(skull != null && !isHolding)
	    {
            agent.isStopped = true;
		    isHolding = true;

		    cuanColl = skull.GetComponent<BoxCollider>();
		    cuanRb = skull.GetComponent<Rigidbody>();
            skullyHealth = skull.GetComponent<CuanHealth>();
		    cuanRb.isKinematic = true;
		    cuanColl.enabled = false;
            skullyHealth.isHeld = true;
            CharacterManager.Instance.HandleHolding();

		    cuanRb.transform.parent = cuanPosition;
		    cuanRb.transform.localPosition = Vector3.zero;
		    cuanRb.transform.localRotation = Quaternion.identity;
	    }
    }

    public void SkullDisaster()
    {
        if(skull != null && isHolding)
	    {
		    cuanRb.transform.parent = null;

	        cuanRb.isKinematic = false;
            Vector3 dropDirection = cuanPosition.transform.up * 5f;
            cuanRb.AddForce(dropDirection, ForceMode.Impulse);
		    cuanColl.enabled = true;
            skullyHealth.isHeld = false;
		    isHolding = false;
            cuanRb = null;

            Debug.Log("I dropa da skull");
		    CharacterManager.Instance.HandleHolding();
            Stunned();
	    }
    }

    void Stunned()
    {
        isStunned = true;
        stunTimer = stunTime;
        agent.isStopped = true;
        agent.ResetPath();
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
