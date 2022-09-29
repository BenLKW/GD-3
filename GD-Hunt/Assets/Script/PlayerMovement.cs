using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Animation")]
    
    public Animator animator;
    float velocity=0;
    public float acceleration = 0.2f;
    int VelocityHash;
    

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float runSpreed;
    public float dashSpeed;
    public float air;
    public float groundDrag;
    public float jumpForce;
    public float jumpcooldown;
    bool readyToJump = true;
    bool dashing;
    public float dashForce;
    public float dashDuraction;
    public float dashCd;
    private float dashCdTimer;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope check")]
    public float maxSlopeAngle;
    RaycastHit slopeHit;

    public Transform orientation;

    [Header("Key")]
    public KeyCode RunKey = KeyCode.LeftShift;
    public KeyCode JumpKey = KeyCode.Space;
    public KeyCode DashKey = KeyCode.E;
    

    float horizontalinput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        running,
        dashing,
        air
    }
    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        VelocityHash = Animator.StringToHash("Velocity");
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        Playerinput();
        SpeedControl();
        stateHandler();
        
        

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        if (dashCdTimer > 0)
        {
            dashCdTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        AnimationState();
    }
    void Playerinput()
    {
        horizontalinput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(JumpKey) && readyToJump && grounded)
        {
            
            readyToJump = false;
            
            Jump();
            Invoke(nameof(ResetJump), jumpcooldown);
        }

        if (Input.GetKeyDown(DashKey) && grounded)
        {
            Dash();
        }
    }


    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalinput;

        if (Onslope())
        {
            rb.AddForce(GetSlopMoveDirection() * moveSpeed * 5f, ForceMode.Force);
        }
        else if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 1f * air, ForceMode.Force);
        }

        rb.useGravity = !Onslope();
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        
        readyToJump = true;
    }


    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        
    }

    private void stateHandler()
    {
        if(dashing && grounded)
        {
            state = MovementState.dashing;
            moveSpeed = dashSpeed;
        }
        else if (grounded && Input.GetKey(RunKey))
        {
            state = MovementState.running;
            moveSpeed = runSpreed;
            
        }
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }

    private bool Onslope()
    {
        if (Physics.Raycast(transform.position, Vector3.down,out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    public void AnimationState()
    {
        if (state == MovementState.running && Input.GetButton("Horizontal") || Input.GetButton("Vertical") && state == MovementState.running)
        {
            if (velocity < 0.3f)
            {
                velocity += Time.deltaTime * acceleration * 20;
            }
            else if (velocity < 1f)
            {
                velocity += Time.deltaTime * acceleration*5;
            }
        }
        else if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (velocity > 0.3f)
            {
                velocity -= Time.deltaTime * acceleration*5;
            }
            if (velocity < 0.3f)
            {
                velocity += Time.deltaTime * acceleration*10;
            }
        }
        else if (velocity > 0.0f)
        {
            velocity -= Time.deltaTime * acceleration*10;
        }

        if (velocity < 0.0f)
        {
            velocity = 0;
        }

        if(state == MovementState.air)
        {
            animator.SetBool("OnAir", true);
        }
        else
        {
            animator.SetBool("OnAir", false);
        }

        animator.SetFloat(VelocityHash, velocity);
    }

    void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        dashing = true;
        animator.SetBool("IsDashing", true);
        Vector3 forceApply = moveDirection.normalized * dashForce;
        delayedForceToApply = forceApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuraction);
    }
    private Vector3 delayedForceToApply;
    void DelayedDashForce()
    {
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }
    void ResetDash()
    {
        dashing = false;
        animator.SetBool("IsDashing", false);
    }
}
