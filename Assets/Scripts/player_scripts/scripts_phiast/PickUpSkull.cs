using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSkull : MonoBehaviour
{
	public bool inRange = false;
	public bool isHolding = false;
	[SerializeField]float throwForce = 300f;
	[SerializeField]float maxDistance = 10f;
	private float distance;

	private Rigidbody rb;
	private Collider cuan;
	private Rigidbody skullRb;//this is a reference for when we pick up the skull
	public Transform objectPosition;

	private PlayerControls playerControls;

void Start()
{
	rb = GetComponent<Rigidbody>();
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
		isHolding = true;
		skullRb = cuan.GetComponent<Rigidbody>();
		skullRb.linearVelocity = Vector3.zero;
		skullRb.isKinematic = true;

		skullRb.transform.parent = objectPosition;
		skullRb.transform.localPosition = Vector3.zero;
		skullRb.transform.localRotation = Quaternion.identity;
	}
	

}

void ThrowSkull()
{

}
void SetUpControls()
{
	playerControls.Enable();
	playerControls.Skull.Interact.performed += HandleSkull;
}

void OnDisable()
{
	playerControls.Disable();
	playerControls.Skull.Interact.performed -= HandleSkull;
}

void HandleSkull(InputAction.CallbackContext context)
{
	if(inRange && !isHolding)
	{
		PickUp();
	}
	if(isHolding)
	{
		ThrowSkull();
	}
}
}
