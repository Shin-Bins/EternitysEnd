using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSkull : MonoBehaviour
{
	public bool inRange = false;
	public bool isHolding = false;
	private bool isAiming = false;
	public float throwForce;
	public float upForce;
	public int trajectSegments = 50;
	public float trajectTimestep = 0.1f;

	private Rigidbody rb;
	private Collider cuan;
	private Rigidbody skullRb;//this is a reference for when we pick up the skull
	public Transform objectPosition;
	private SphereCollider pickUpTrigger;
	private LineRenderer trajectLine;
	private PlayerControls playerControls;

void Start()
{
	rb = GetComponent<Rigidbody>();
	pickUpTrigger = GetComponent<SphereCollider>();
	trajectLine = GetComponent<LineRenderer>();
	trajectLine.enabled = false;
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
			return;
		}
	isAiming = true;
	Vector3[] positions = new Vector3[trajectSegments];
	Vector3 currentPos = objectPosition.position;
	Vector3 throwDirection = objectPosition.transform.forward * throwForce + objectPosition.transform.up * upForce;
	Vector3 currentVel = throwDirection / skullRb.mass;
		
	for(int i = 0; i < trajectSegments; i++)
		{
			positions[i] = currentPos;
			currentVel += Physics.gravity * trajectTimestep;
			currentPos += currentVel * trajectTimestep;
		}

	trajectLine.positionCount = trajectSegments;
	trajectLine.SetPositions(positions);
	trajectLine.enabled = true;
	}

void PickUp()
{
	if(cuan != null)
	{
		pickUpTrigger.enabled = false;
		isHolding = true;
		skullRb = cuan.GetComponent<Rigidbody>();
		skullRb.linearVelocity = Vector3.zero;
		skullRb.isKinematic = true;

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
	isHolding = false;
	skullRb = null;
	pickUpTrigger.enabled = true;
	}
}

public void OnThrow()
{
	if(cuan != null && isHolding)
	{
	skullRb.transform.parent = null;
	skullRb.isKinematic = false;
	Vector3 throwDirection = objectPosition.transform.forward * throwForce + objectPosition.transform.up * upForce;
	skullRb.AddForce(throwDirection, ForceMode.Impulse);


	isHolding = false;
	skullRb = null;
	pickUpTrigger.enabled = true;
	isAiming = false;
	trajectLine.enabled = false;
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

}
