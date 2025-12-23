using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSkull : MonoBehaviour
{
	private bool inRange = false;
	public bool isHolding = false;
	private bool isAiming = false;

	public float throwForce;
	public float upForce;

	public float aimThrowForce;//coming soon in the throw update
	public float aimUpForce;
	public float throwForceStep = 1f; // how much to increase/decrease per scroll
	public float upForceStep = 0.5f;
	public float minThrowForce = 5f; // min values
	public float maxThrowForce = 30f; // max values
	public float minUpForce = 1f;
	public float maxUpForce = 15f;

	private int trajectSegments = 50;
	private float trajectTimestep = 0.1f;
	public Transform endMarker;
	[SerializeField]private LayerMask collisionLayers;
	private LineRenderer trajectLine;

	private Rigidbody rb;
	private Collider obj;
	private Rigidbody objRb;//this is a reference for when we pick up the obj
	private BoxCollider objColl;//turn off collision with skull when carried. Was having some funky effects on phiast
	public Transform objectPosition;//this is where cuan is held
	private SphereCollider pickUpTrigger;//checks to see if phiast is in range. Probably another way to do this but it works
	private panzermove phiast;
	private PlayerControls playerControls;

void Start()
{
	rb = GetComponent<Rigidbody>();
	phiast = GetComponent<panzermove>();
	pickUpTrigger = GetComponent<SphereCollider>();
	trajectLine = GetComponent<LineRenderer>();
	trajectLine.enabled = false;
}

void Update()
{
	if(isAiming && isHolding)
	{
		UpdateTrajectory();
	}
}

void OnTriggerEnter(Collider other)
{
	if(other.CompareTag("skull") || other.CompareTag("PickUp"))
	{
		inRange = true;
		obj = other;
	}
}

void OnTriggerExit(Collider other)
{
	if(other.CompareTag("skull") || other.CompareTag("PickUp"))
	{
		inRange = false;
		obj = null;
	}
}

   public void OnAim()
    {
        if(!isHolding || objRb == null)
        {
            trajectLine.enabled = false;
            if(endMarker != null) endMarker.gameObject.SetActive(false);
            isAiming = false;
            return;
        }
        
        // Toggle aiming on/off
        isAiming = !isAiming;
        
        if(!isAiming)
        {
            trajectLine.enabled = false;
            if(endMarker != null) endMarker.gameObject.SetActive(false);
        }
    }

void PickUp()
{
	if(obj != null)
	{
		pickUpTrigger.enabled = false;
		isHolding = true;
		phiast.holdingSkull = true;

		objColl = obj.GetComponent<BoxCollider>();
		objRb = obj.GetComponent<Rigidbody>();
		objRb.isKinematic = true;
		objColl.enabled = false;

		objRb.transform.parent = objectPosition;
		objRb.transform.localPosition = Vector3.zero;
		objRb.transform.localRotation = Quaternion.identity;
	}
}

void Drop()
{
	if(obj != null && isHolding)
	{
	objRb.transform.parent = null;

	objRb.isKinematic = false;
	objColl.enabled = true;

	isHolding = false;
	phiast.holdingSkull = false;

	objRb = null;
	pickUpTrigger.enabled = true;
	endMarker.gameObject.SetActive(false);
	}
}

public void OnThrow()
{
	if(obj != null && isHolding)
	{
		objRb.transform.parent = null;
		objRb.isKinematic = false;
		objColl.enabled = true;

		float currentThrowForce = isAiming ? aimThrowForce : throwForce;
		float currentUpForce = isAiming ? aimUpForce : upForce;
		
		Vector3 throwDirection = objectPosition.transform.forward * currentThrowForce + objectPosition.transform.up * currentUpForce;
		objRb.AddForce(throwDirection, ForceMode.Impulse);


		isHolding = false;
		phiast.holdingSkull = false;
		objRb = null;
		pickUpTrigger.enabled = true;
		isAiming = false;
		trajectLine.enabled = false;
		endMarker.gameObject.SetActive(false);
	}
}


void OnInteract()
{
	if(inRange && !isHolding)
	{
		PickUp();
	}
	else{
		Drop();//fixed an issue, now E will drop the skull if we're holding it'
	}
	
}

public void OnCycleThrow(InputValue value)
{
	if(!isAiming || !isHolding) return;
	
	float scroll = value.Get<Vector2>().y;
	
	if(scroll > 0f)
	{
		// scroll up = increase throw
		aimThrowForce = Mathf.Clamp(aimThrowForce + throwForceStep, minThrowForce, maxThrowForce);
		aimUpForce = Mathf.Clamp(aimUpForce + upForceStep, minUpForce, maxUpForce);
	}
	else if(scroll < 0f)
	{
		// scroll down = decrease throw
		aimThrowForce = Mathf.Clamp(aimThrowForce - throwForceStep, minThrowForce, maxThrowForce);
		aimUpForce = Mathf.Clamp(aimUpForce - upForceStep, minUpForce, maxUpForce);
	}
}


void UpdateTrajectory()//Beware the code below, for the abyss stares back at thee
{
	Vector3[] positions = new Vector3[trajectSegments];
        Vector3 currentPos = objectPosition.position;
        
        float currentThrowForce = isAiming ? aimThrowForce : throwForce;
        float currentUpForce = isAiming ? aimUpForce : upForce;
        
        Vector3 throwDirection = objectPosition.transform.forward * currentThrowForce + objectPosition.transform.up * currentUpForce;
        Vector3 currentVel = throwDirection / objRb.mass;

        float drag = objRb.linearDamping;
        int finalSegment = trajectSegments;
        bool hitSomething = false;
        Vector3 hitPoint = Vector3.zero;
		float offSet = 0.025f;
        
        for(int i = 0; i < trajectSegments; i++)
        {
            positions[i] = currentPos;

            if(i < trajectSegments - 1)
            {
                Vector3 nextPos = currentPos + currentVel * trajectTimestep + 0.5f * Physics.gravity * trajectTimestep * trajectTimestep;
                
                RaycastHit hit;
                if(Physics.Raycast(currentPos, (nextPos - currentPos).normalized, out hit, (nextPos - currentPos).magnitude, collisionLayers))
                {
                    hitPoint = hit.point;
                    hitSomething = true;
                    finalSegment = i + 1;
                    positions[finalSegment - 1] = hitPoint;
                    break;
                }       
                currentPos = nextPos;
                currentVel += Physics.gravity * trajectTimestep;

                currentVel *= (1f - drag * trajectTimestep);
            }
        }

        trajectLine.positionCount = finalSegment;
        trajectLine.SetPositions(positions);
        trajectLine.enabled = true;

        if(endMarker != null)
        {
            endMarker.gameObject.SetActive(true);
            if(hitSomething)
            {
                endMarker.position = hitPoint + Vector3.up * offSet;
            }
            else
            {
                endMarker.position = positions[finalSegment - 1];
            }
        }
}

}
