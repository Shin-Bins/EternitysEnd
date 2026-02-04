using UnityEngine;
using UnityEngine.InputSystem;

public class panzermove : MonoBehaviour
{

//movin and jumpin and holdin bidnuss
    private Vector2 input;
    private float verticalVelocity = 0f;
    
    public float moveSpeed = 7f;
    public float turnSpeed = 100f;
    public float jumpHeight = 2f;
    public float gravity = 9.81f;
    private float liftSpeed = 4f;
    public bool holdingSkull = false;
    public GameObject ghostMan; // Crazy workaround for moving platforms. Thank you 4 year old unity forums
    
    //technical bidnuss
    private Vector3 movement;
    private CharacterController controller;
    private Quaternion ghostRotation;

    //Audio bidnuss
    private AudioSource src;
    public AudioClip[] jumpAud;
    public AudioClip grassSteps;
    public AudioClip woodSteps;
    public AudioClip stoneSteps;
    public AudioClip waterSteps;
    private AudioClip phiastSteps;

    private float footstepInterval = 0.5f;//how long between steps
    private float footstepTimer = 0f;

    //Aesthetic bidnuss
    public ParticleSystem jumpDustEffect;
    private Animator anim;
    private float timeForIdle = 10f;
    private float idleTimer = 0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    
    void Start()
    {
        if(ghostMan != null)
        {
            ghostMan.transform.position = transform.position;
            ghostRotation = ghostMan.transform.rotation;
        }
        src = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        bool isGrounded = controller.isGrounded;
        
        if (input.x != 0)
        {
            float turn = input.x * turnSpeed * Time.deltaTime;
            transform.Rotate(0, turn, 0);
        }
        
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        
        Vector3 direction = transform.forward * input.y * moveSpeed;
        direction.y = verticalVelocity;
        movement = direction * Time.deltaTime;

        //anim params for run
        float playerSpeed = Mathf.Abs(input.y) * moveSpeed;
        anim.SetFloat("Player_Speed", playerSpeed);

        //anim for idle
        if(input.x == 0 && input.y == 0)
        {
            idleTimer += Time.deltaTime;
            if(idleTimer >= timeForIdle)
            {
                anim.SetTrigger("IdleMaster");
                idleTimer = 0f;
            }
        }
        else{
            idleTimer = 0f;
        }

        bool isMoving = isGrounded && input.y != 0;
        if (isMoving)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                PlayPhiastSteps();
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            footstepTimer = 0f; // Reset when not moving
        }
    }
    
    void LateUpdate()
    {
        if (ghostMan == null) return;
        
        // rotate with the platform
        Quaternion ghostRotationDelta = ghostMan.transform.rotation * Quaternion.Inverse(ghostRotation);
        
        //keep the ghost and phiast synced up
        Vector3 translation = ghostMan.transform.position - transform.position;
        
        //Keep player movement working while on the platform
        controller.Move(translation + movement);
        
        //really annoying stuff to not brick our player movement with the moving platform capability
        transform.rotation = ghostRotationDelta * transform.rotation;
        ghostMan.transform.position = transform.position;
        ghostRotation = ghostMan.transform.rotation;
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (ghostMan == null) return;
        
        // parent the ghost to the platform
        if (hit.collider.GetComponent<MovingPlatform>() != null)
        {
            ghostMan.transform.parent = hit.transform;
            ghostMan.transform.position = transform.position;
            ghostRotation = ghostMan.transform.rotation;
        }
        else if (hit.normal.y > 0.5f) // unparent ghost when on normal ground
        {
            ghostMan.transform.parent = null;
            ghostRotation = ghostMan.transform.rotation;
        }
    }    
    
    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
    }

    void PlayPhiastSteps()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            if(hit.collider.CompareTag("Grass"))
                phiastSteps = grassSteps;
            else if(hit.collider.CompareTag("Wood"))
                phiastSteps = woodSteps;
            else if(hit.collider.CompareTag("Stone"))
                phiastSteps = stoneSteps;
            else if(hit.collider.CompareTag("Water"))
                phiastSteps = waterSteps;
        }
        if(phiastSteps != null)
        {
            src.pitch = Random.Range(0.8f, 1.2f);
            src.PlayOneShot(phiastSteps);
        }
    }
    
    public void OnJump()
    {
        if (controller.isGrounded && !holdingSkull)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * 2f * gravity);
            if(jumpAud.Length != 0)
            {
                int randomClip = Random.Range(0, jumpAud.Length);
                src.clip = jumpAud[randomClip];
                src.pitch = Random.Range(0.9f, 1.1f);
                src.Play();
            }

            // Trigger the dust effect
            if (jumpDustEffect != null)
            {
                jumpDustEffect.Play();
                Debug.Log("dust");
            }
        }
    }
}
