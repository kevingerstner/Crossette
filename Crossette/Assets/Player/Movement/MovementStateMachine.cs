using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementStateMachine : IStateMachine<MovementState>
{
    public float inputX = 0.0f;
    public int facingDirection = 1;
    public float delayToIdle = 0.0f;
    public Vector3 movement = Vector3.zero;

    public bool grounded = false;
    public bool canMove = true;

    private IdleState idleState;
    private RunState runState;
    private JumpState jumpState;

    Player player;
    Animator animator;
    [HideInInspector] public Sensor_HeroKnight m_groundSensor;
    [HideInInspector] public Sensor_HeroKnight m_wallSensorR1;
    [HideInInspector] public Sensor_HeroKnight m_wallSensorR2;
    [HideInInspector] public Sensor_HeroKnight m_wallSensorL1;
    [HideInInspector] public Sensor_HeroKnight m_wallSensorL2;

    // Start is called before the first frame update
    public override void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();

        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();

        idleState = new IdleState(player, this, animator);
        runState = new RunState(player, this, animator);
        jumpState = new JumpState(player, this, animator);
        Initialize(runState);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        //Wall Slide
        animator.SetBool("WallSlide", (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State()));

        //Check if character just landed on the ground
        if (!grounded && m_groundSensor.State())
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
            ChangeState(runState);
        }

        //Character just started falling
        if (grounded && !m_groundSensor.State())
        {
            grounded = false;
            animator.SetBool("Grounded", grounded);
        }

        if (!grounded)
        {
            //Set AirSpeed in animator
            animator.SetFloat("AirSpeedY", player.m_body2d.velocity.y);
        }
    }

    public void OnMove(InputValue value)
    {
        //if (m_killed || m_stunned) return;
        inputX = value.Get<float>();

        if (!canMove) return;

        if (Mathf.Abs(inputX) > Mathf.Epsilon)
            ChangeState(runState);
        else
            ChangeState(idleState);
    }

    private void OnJump()
    {
        //if (!m_grounded || m_killed || m_stunned) return;
        if (!grounded) return;
        Debug.Log("JUMP");

        ChangeState(jumpState);
    }

    public void DisableMovement()
    {
        canMove = false;
        movement.x = 0;
        player.m_body2d.velocity = Vector3.zero;
        animator.SetInteger("AnimState", 0);
        ChangeState(idleState);
    }
    public void EnableMovement()
    {
        canMove = true;
        movement.x = 0;
        player.m_body2d.velocity = Vector3.zero;

        if (Mathf.Abs(inputX) > Mathf.Epsilon)
            ChangeState(runState);
        else
            ChangeState(idleState);
    }
}

public abstract class MovementState : IState
{
    protected readonly Player player;
    protected readonly MovementStateMachine sm;
    protected readonly Animator animator;

    protected MovementState(Player player, MovementStateMachine stateMachine, Animator animator)
    {
        this.player = player;
        this.sm = stateMachine;
        this.animator = animator;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public virtual void FixedUpdate() { }
    public abstract void OnExit();
}