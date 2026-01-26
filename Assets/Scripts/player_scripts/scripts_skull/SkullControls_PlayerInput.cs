//using UnityEditor;
//using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkullControls_PlayerInput : MonoBehaviour
{
    bool rotatingLeft;
    bool rotatingRight;
    public AnimationCurve windupCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
     public float maxRotationSpeed = 180f;
    public float rotationAcceleration = 2f;
    
    private float currentRotationTime = 0f;

    private bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;//how big of an area we check for the ground. Keep small so we dont have a floating skull :)
    public LayerMask groundLayer;
    public float slamForce = 10f;
    private Rigidbody srb;

    private Transform anPhiast;
    public float lookAtSpeed = 5f; // Speed rotation toward Phiast
    private bool isLookingAtPhiast = false;
    private Quaternion targetRotation;

    public AudioClip bonk;
    private AudioSource src;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        srb = GetComponent<Rigidbody>();
        src = GetComponent<AudioSource>();
        GameObject phiastRef = GameObject.FindGameObjectWithTag("phiast");
        if(phiastRef != null)
        {
            anPhiast = phiastRef.transform;
        }
    }

    public void OnFindPhiast()
    {
        if (anPhiast != null)
        {
            
            Vector3 directionToPhiast = anPhiast.position - transform.position;
            directionToPhiast.y = 0; // Keep rotation horizontal
            
            if (directionToPhiast != Vector3.zero)
            {
                targetRotation = Quaternion.LookRotation(directionToPhiast);
                isLookingAtPhiast = true;//stops rotation when looking at phiast
            }
        }
    }

    public void OnRotateLeft(InputValue val)
    {
        Debug.Log("Left " + val.Get<float>());
        rotatingLeft = val.Get<float>() > 0.5f;
    }
    public void OnRotateRight(InputValue val)
    {
        Debug.Log("Right " + val.Get<float>());
        rotatingRight = val.Get<float>() > 0.5f;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            if (isLookingAtPhiast)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookAtSpeed * Time.deltaTime);

                if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
                {
                    isLookingAtPhiast = false;
                }
            }
            else
            {
                HandleRotation();
            }
        }
        else
        {
            currentRotationTime = 0f;
        }
    }

    void HandleRotation()
    {
        bool isRotating = rotatingLeft || rotatingRight;
        
        if (isRotating)
        {
            currentRotationTime += Time.deltaTime * rotationAcceleration;
            currentRotationTime = Mathf.Clamp01(currentRotationTime);
        }
        else
        {
            currentRotationTime -= Time.deltaTime * rotationAcceleration;
            currentRotationTime = Mathf.Max(0f, currentRotationTime);
        }
        
        float curveValue = windupCurve.Evaluate(currentRotationTime);
        float rotationAmount = curveValue * maxRotationSpeed * Time.deltaTime;

        if (rotatingLeft)
        {
            transform.Rotate(Vector3.down * rotationAmount);
        }
        else if (rotatingRight)
        {
            transform.Rotate(Vector3.up * rotationAmount);
        }
    }

    public void OnSlam()
    {
        if (!isGrounded && transform.parent == null)
        {
            srb.linearVelocity = -transform.up * slamForce;
        }
    }

    void OnCollisionEnter(Collision collider)
    {
        if(!isGrounded)
        {
            src.PlayOneShot(bonk);
        }
    }
}
