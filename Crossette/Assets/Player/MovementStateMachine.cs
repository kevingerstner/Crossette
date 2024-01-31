using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public abstract class MovementState : IState
{
    protected readonly Player player;
    protected readonly MovementStateMachine stateMachine;
    
    protected MovementState(Player player, MovementStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateMachine = stateMachine;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public virtual void FixedUpdate() { }
    public abstract void OnExit();
}

public class MovementStateMachine : IStateMachine<MovementState>
{
    Player player;

    public float inputX = 0.0f;
    public int m_facingDirection = 1;
    public float m_delayToIdle = 0.0f;
    public Vector3 m_movement = Vector3.zero;

    private IdleState idleState;
    private RunState runState;

    // Start is called before the first frame update
    public override void Start()
    {
        player = GetComponent<Player>();
        idleState = new IdleState(player, this);
        runState = new RunState(player, this);
        Initialize(runState);
    }

    public override void Initialize(MovementState initialState)
    {
        base.Initialize(initialState);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public void OnMove(InputValue value)
    {
        //if (m_killed || m_stunned) return;

        inputX = value.Get<float>();

        // Swap direction of sprite depending on walk direction
        if (Mathf.Abs(inputX) > Mathf.Epsilon)
            ChangeState(runState);
        else
            ChangeState(idleState);
    }
}
