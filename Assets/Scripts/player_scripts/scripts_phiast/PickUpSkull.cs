using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSkull : MonoBehaviour
{
	public bool inRange = false;
	public bool isHolding = false;
	private bool isAiming = false;

	public float throwForce;
	public float upForce;
	public float aimThrowForce;//voming soon in the throw update
	public float aimUpForce;

	private int trajectSegments = 50;
	private float trajectTimestep = 0.1f;
	public Transform endMarker;
	[SerializeField]private LayerMask collisionLayers;

	private Rigidbody rb;
	private Collider cuan;
	private Rigidbody skullRb;//this is a reference for when we pick up the skull
	private BoxCollider skullColl;//turn off collision with skull when carried. Was having some funky effects on phiast
	public Transform objectPosition;//this is where cuan is held
	private SphereCollider pickUpTrigger;//checks to see if phiast is in range. Probably another way to do this but it works
	private LineRenderer trajectLine;
	private PlayerControls playerControls;

void Start()
{
	rb = GetComponent<Rigidbody>();
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
	if(other.CompareTag("skull"))
	{
		inRange = true;
		cuan = other;
	}
}

void OnTriggerExit(Collider other)
{
	if(other.CompareTag("skull"))
	{
		inRange = false;
		cuan = null;
	}
}

   public void OnAim()
    {
        if(!isHolding || skullRb == null)
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
	if(cuan != null)
	{
		pickUpTrigger.enabled = false;
		isHolding = true;
		skullColl = cuan.GetComponent<BoxCollider>();
		skullRb = cuan.GetComponent<Rigidbody>();
		skullRb.linearVelocity = Vector3.zero;
		skullRb.isKinematic = true;
		skullColl.enabled = false;
		skullRb.transform.parent = objectPosition;
		skullRb.transform.localPosition = Vector3.zero;
		skullRb.transform.localRotation = Quaternion.identity;
	}
}

void Drop()
{
	if(cuan != null && isHolding)
	{
	skullRb.transform.parent = null;
	skullRb.isKinematic = false;
	skullColl.enabled = true;
	isHolding = false;
	skullRb = null;
	pickUpTrigger.enabled = true;
	endMarker.gameObject.SetActive(false);
	}
}

public void OnThrow()
{
	if(cuan != null && isHolding)
	{
		skullRb.transform.parent = null;
		skullRb.isKinematic = false;
		skullColl.enabled = true;
		Vector3 throwDirection = objectPosition.transform.forward * throwForce + objectPosition.transform.up * upForce;
		skullRb.AddForce(throwDirection, ForceMode.Impulse);


		isHolding = false;
		skullRb = null;
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

void UpdateTrajectory()//Beware the code below, for the abyss stares back at thee
{
	 Vector3[] positions = new Vector3[trajectSegments];
        Vector3 currentPos = objectPosition.position;
        Vector3 throwDirection = objectPosition.transform.forward * throwForce + objectPosition.transform.up * upForce;
        Vector3 currentVel = throwDirection / skullRb.mass;//above are the calculations of the throw itself

        float drag = skullRb.linearDamping;
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
