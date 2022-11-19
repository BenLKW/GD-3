using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Animation")]

    public Animator animator;
    float velocity = 0;
    public float acceleration = 0.2f;
    int VelocityHash;


    [Header("Movement")]
    public Transform orientation;
    Vector3 moveDirection;
    Rigidbody rb;
    private float moveSpeed;
    public float walkSpeed;
    public float runSpreed;
    public float dashSpeed;
    public float AttackingSpeed;
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

    [Header("Attask")]
    public int CountAttack;
    public WeaponSystem weaponSystem;

    [Header("Throwing")]
    
    public float ThrowForce;
    public float ThrowUpwardForce;
    bool ReadyToThrow;
    public int TotalThrow;
    public float ThrowCoolDown;
    public Transform ThrowDir;
    public Transform AtkPoi;
    public GameObject ObjectToTrow;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    bool grounded;

    [Header("Slope check")]
    public float maxSlopeAngle;
    RaycastHit slopeHit;

    [Header("Key")]
    public KeyCode RunKey = KeyCode.LeftShift;
    public KeyCode JumpKey = KeyCode.Space;
    public KeyCode DashKey = KeyCode.E;
    public KeyCode DrawWeaponKey = KeyCode.X;
    public KeyCode AttackKey = KeyCode.Mouse0;
    public KeyCode ThrowKey = KeyCode.Mouse1;

    [Space]
    float horizontalinput;
    float verticalInput;

    
    [Header("State")]
    public MovementState Move;
    public CombatState Combat;
    public ActionState Action;
    public enum MovementState
    {
        walking,
        running,
        dashing,
        jumping,
        air
    }

    public enum ActionState
    {
        Move,
        Attack
    }
    public enum CombatState
    {
        WeaponInShealth, Drawweapon
    }
    // Start is called before the first frame update
    void Start()
    {
        weaponSystem = gameObject.GetComponent<WeaponSystem>();
        animator = GetComponent<Animator>();
        VelocityHash = Animator.StringToHash("Velocity");
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ReadyToThrow = true;

    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

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

        if (Input.GetKeyDown(DashKey) && grounded && Input.GetButton("Horizontal") || Input.GetButton("Vertical") && Input.GetKeyDown(DashKey) && grounded)
        {
            Dash();
        }

        if (Input.GetKeyDown(DrawWeaponKey))
        {
            if (Combat == CombatState.WeaponInShealth)
            {

                Combat = CombatState.Drawweapon;
            }
            else if (Combat == CombatState.Drawweapon)
            {

                Combat = CombatState.WeaponInShealth;
            }
        }

        if (grounded && Combat == CombatState.Drawweapon && Input.GetKeyDown(AttackKey) )
        {
            
            Action = ActionState.Attack;
            CountAttack++;
            
        }
        else
        {
            if(CountAttack >= 16)
            {
                
                CountAttack = 0;
            }
        }

        if (grounded && Input.GetKeyDown(ThrowKey) && ReadyToThrow && TotalThrow > 0 && Action != ActionState.Attack)
        {
            
            animator.SetTrigger("Throwing");
        }
        
    }


    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalinput;

        if (Onslope())
        {
            rb.AddForce(GetSlopMoveDirection() * moveSpeed * 5f, ForceMode.Force);
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 8f, ForceMode.Force);
            }
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
        if (Action == ActionState.Move)
        {
            
            if (dashing && grounded)
            {
                Move = MovementState.dashing;
                moveSpeed = dashSpeed;
            }
            else if (grounded && Input.GetKey(RunKey))
            {
                Move = MovementState.running;
                moveSpeed = runSpreed;

            }
            else if (grounded)
            {
                Move = MovementState.walking;
                moveSpeed = walkSpeed;
            }
            else
            {
                if (readyToJump==false)
                {
                    Move = MovementState.jumping;
                }
                else
                {
                    Move = MovementState.air;
                }
                
            }
        }
        else if (Action == ActionState.Attack)
        {
            moveSpeed = AttackingSpeed;
        }


    }

    private bool Onslope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
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
        if (Move == MovementState.running && Input.GetButton("Horizontal") || Input.GetButton("Vertical") && Move == MovementState.running)
        {
            if (velocity < 0.3f)
            {
                velocity += Time.deltaTime * acceleration * 20;
            }
            else if (velocity < 1f)
            {
                velocity += Time.deltaTime * acceleration * 5;
            }
        }
        else if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (velocity > 0.3f)
            {
                velocity -= Time.deltaTime * acceleration * 5;
            }
            if (velocity < 0.3f)
            {
                velocity += Time.deltaTime * acceleration * 10;
            }
        }
        else if (velocity > 0.0f)
        {
            velocity -= Time.deltaTime * acceleration * 10;
        }

        if (velocity < 0.0f)
        {
            velocity = 0;
        }

        if (Move == MovementState.air)
        {
            animator.SetBool("OnAir", true);
        }
        else
        {
            animator.SetBool("OnAir", false);
        }

        if (Move == MovementState.jumping)
        {
            animator.SetBool("IsJumping", true);
        }
        else if (Move != MovementState.jumping)
        {
            animator.SetBool("IsJumping", false);
        }

        if (Combat == CombatState.Drawweapon)
        {
            animator.SetBool("WeaponDraw", true);
        }
        else if (Combat == CombatState.WeaponInShealth)
        {
            animator.SetBool("WeaponDraw", false);
        }

        if (CountAttack == 1)
        {
            animator.SetInteger("Attack", 1);
        }

        if (animator.GetCurrentAnimatorStateInfo(3).IsName("Combo Attack Ver1"))
        {
            if (CountAttack > 1)
            {
                animator.SetInteger("Attack", 2);
            }
            else
            {
                ReturntoMove();

            }

        }
        else if (animator.GetCurrentAnimatorStateInfo(3).IsName("Combo Attack Ver2"))
        {
            if (CountAttack > 2)
            {
                animator.SetInteger("Attack", 3);
            }
            else
            {
                ReturntoMove();

            }
        }        

        animator.SetFloat(VelocityHash, velocity);
    }

    void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        dashing = true;
        animator.SetTrigger("IsDashing");
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

    }


    void EndAttackAnimation()
    {
        if (CountAttack >= 3)
        {
            ReturntoMove();
        }
    }
    void ReturntoMove()
    {
        CountAttack = 0;
        animator.SetInteger("Attack", 0);
        Action = ActionState.Move;

    }

    public void Throw()
    {
        ReadyToThrow = false;

        GameObject stone = Instantiate(ObjectToTrow, AtkPoi.position, ThrowDir.rotation);
        Rigidbody stoneRb = stone.GetComponent<Rigidbody>();
        Vector3 ForceToAdd = ThrowDir.forward * ThrowForce + transform.up * ThrowUpwardForce;

        stoneRb.AddForce(ForceToAdd, ForceMode.Impulse);

        TotalThrow--;

        Invoke(nameof(ResetThrow), ThrowCoolDown);
    }

    private void ResetThrow()
    {
        ReadyToThrow = true;
    }
}
