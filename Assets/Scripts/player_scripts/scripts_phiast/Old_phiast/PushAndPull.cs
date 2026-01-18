using UnityEngine;
using UnityEngine.InputSystem;

public class PushAndPull : MonoBehaviour
{
       private bool blockInRange = false;
       private bool holdingBlock = false;
       private bool dropBlock = true;// if we're colliding the block when we move, drop block'

       private Collider blockCollider;
       private Vector3 blockSize;
       public LayerMask obstacleLayer;
       public float raycastOffset = 0.1f;//extra space to the ray to avoid clipping through walls
       private Vector3 solidGround;//Sick dark souls reference xdddd. Used for block collison and stopping the player from morphing into the block
       private float collThreshold = 0.01f;//How much we can move into an obstacle before we drop the block. I hate the word block now

       private GameObject heldBlock;//object reference for the block
	   public Transform objectPosition;//same as pick up item spot
	   private SphereCollider pickUpTrigger;
       private panzermove movement;

       void Start()
       {
           movement = GetComponent<panzermove>();
           pickUpTrigger = GetComponent<SphereCollider>();
       }

       void OnInteract()
       {
           if (holdingBlock)
            {
                LetGo();
            }
            else if (blockInRange && heldBlock != null)
            {
                Lift();
            }
       }
       
    private void Lift()
    {
     if (heldBlock == null) return;
        blockCollider = heldBlock.GetComponent<Collider>();
        
        blockSize = blockCollider.bounds.size;
        blockCollider.enabled = false;

        heldBlock.transform.SetParent(objectPosition);
        solidGround = heldBlock.transform.position;
        holdingBlock = true;
        movement.isLifting = true;
        pickUpTrigger.enabled = false;
        Debug.Log("Lift");
    }

    private void LetGo()
    {
        if (heldBlock == null) return;

        heldBlock.transform.SetParent(null);
        blockCollider.enabled = true;

        holdingBlock = false;
        movement.isLifting = false;
        pickUpTrigger.enabled = true;
        heldBlock = null;
    }

    void LateUpdate()
    {
      if (holdingBlock && heldBlock != null)
        {
            float blockMove = Vector3.Distance(heldBlock.transform.position, solidGround);
            if (blockMove > collThreshold)
            {
                bool blockBlocked = CheckCollision();
                if (blockBlocked)
                {
                    if (dropBlock)
                    {
                        LetGo();
                    }
                }
            }
            if(heldBlock != null)
            {
                solidGround = heldBlock.transform.position;
            }
        }
    }

    private bool CheckCollision()
    {
        Vector3 blockPos = heldBlock.transform.position;
        Vector3 blockCenter = blockPos;
        bool hitObstacle = false;

        Vector3[] directions = {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right,
        };
        
        foreach (Vector3 dir in directions)
        {
            float distance = (blockSize.magnitude / 2f) + raycastOffset;//raycast checks is something is blocking the block
            
           if (Physics.Raycast(blockCenter, dir, out RaycastHit hit, distance, obstacleLayer))
            {
                hitObstacle = true;
            }
        }
        return hitObstacle;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Movable") && !holdingBlock)
        {
            blockInRange = true;
            heldBlock = other.gameObject;
            Debug.Log("BLOCK");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Movable") && heldBlock == null)
        {
            blockInRange = false;
            heldBlock = null;
        }
    }
}