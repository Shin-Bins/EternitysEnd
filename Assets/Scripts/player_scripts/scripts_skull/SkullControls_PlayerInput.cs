using UnityEditor.AnimatedValues;
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

    //public Transform anPhiast;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        srb = GetComponent<Rigidbody>();   
    }

    public void OnFindPhiast()
    {
       // transform.LookAt(anPhiast); 
    }

    public void OnRotateLeft(InputValue val)
    {
        Debug.Log("Left " + val.Get<float>());
        /*switch*/rotatingLeft = val.Get<float>() > 0.5f;
        {
          /*  case 0: rotatingLeft = false; break;
            case 1: rotatingLeft = true; break;
            default: rotatingLeft=false; break;*/
        }
    }
    public void OnRotateRight(InputValue val)
    {
        Debug.Log("Right " + val.Get<float>());
        /*switch (*/rotatingRight = val.Get<float>() > 0.5f;
        {
         /*   case 0: rotatingRight = false; break;
            case 1: rotatingRight = true; break;
            default: rotatingRight = false; break;*/
        }
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

            if (isGrounded)
        {
            HandleRotation();
        }
        else
        {
            // Reset rotation time when not grounded
            currentRotationTime = 0f;
        }

       /* if (rotatingLeft && isGrounded)
        {
            transform.Rotate(new Vector3(0,-1,0));
        }
        if (rotatingRight && isGrounded)
        {
            transform.Rotate(new Vector3(0, 1, 0));
        }*/
    }



    void HandleRotation()
    {
        bool isRotating = rotatingLeft || rotatingRight;
        
        if (isRotating)
        {
            // Increase time along the curve
            currentRotationTime += Time.deltaTime * rotationAcceleration;
            currentRotationTime = Mathf.Clamp01(currentRotationTime);
        }
        else
        {
            // Decrease time when not rotating (deceleration)
            currentRotationTime -= Time.deltaTime * rotationAcceleration;
            currentRotationTime = Mathf.Max(0f, currentRotationTime);
        }
        
        // Evaluate the curve to get rotation multiplier
        float curveValue = windupCurve.Evaluate(currentRotationTime);
        float rotationAmount = curveValue * maxRotationSpeed * Time.deltaTime;
        
        // Apply rotation
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
}
