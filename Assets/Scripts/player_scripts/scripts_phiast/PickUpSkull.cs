using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSkull : MonoBehaviour
{
	public bool inRange = false;
	public bool isHolding = false;
	public float throwForce;

	private Rigidbody rb;
	private Collider cuan;
	private Rigidbody skullRb;//this is a reference for when we pick up the skull
	public Transform objectPosition;
	private SphereCollider pickUpTrigger;

	private PlayerControls playerControls;

void Start()
{
	rb = GetComponent<Rigidbody>();
	pickUpTrigger = GetComponent<SphereCollider>();
	playerControls = new PlayerControls();
	SetUpControls();
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

void Throw()
{
		if(cuan != null && isHolding)
	{
	skullRb.transform.parent = null;
	skullRb.isKinematic = false;
	skullRb.AddForce(objectPosition.transform.forward * throwForce, ForceMode.Impulse);
	isHolding = false;
	skullRb = null;
	pickUpTrigger.enabled = true;
	}
}

void SetUpControls()
{
	playerControls.Enable();
	playerControls.Skull.Interact.performed += HandleSkull;
	playerControls.Skull.Throw.performed += ThrowSkull;
	playerControls.Skull.Drop.performed += DropSkull;
}

void OnDisable()
{
	playerControls.Disable();
	playerControls.Skull.Interact.performed -= HandleSkull;
	playerControls.Skull.Throw.performed -= ThrowSkull;
	playerControls.Skull.Drop.performed -= DropSkull;
}

void HandleSkull(InputAction.CallbackContext context)
{
	if(inRange && !isHolding)
	{
		PickUp();
	}
}
void ThrowSkull(InputAction.CallbackContext context)
{
	if(isHolding)
	{
		Throw();
	}
}

void DropSkull(InputAction.CallbackContext context)
{
	if(isHolding)
	{
		Drop();
	}
}
}
