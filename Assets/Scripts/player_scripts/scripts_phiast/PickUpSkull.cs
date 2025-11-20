using UnityEngine;
using UnityEngine.InputSystem;
public class PickUpSkull : MonoBehaviour
{
bool isHolding = false;
[SerializeField]float throwForce = 300f;
[SerializeField]float maxDistance = 10f;
float distance;

HoldSkull holdSkull;
Rigidbody rb;
Vector3 objectPosition;

void Start()
{
	rb = GetComponent<Rigidbody>();
	holdSkull = HoldSkull.Instance;
}
}
