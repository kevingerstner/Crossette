using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class StateMachine : MonoBehaviour
{
    Player player;
    public PlayerState currentState;
    Dictionary<Type, PlayerState> nodes = new();

    public float inputX = 0.0f;
    public int m_facingDirection = 1;
    public float m_delayToIdle = 0.0f;
    public Vector3 m_movement = Vector3.zero;

    private IdleState idleState;
    private RunState runState;

    public void Start()
    {
        player = GetComponent<Player>();
        idleState = new IdleState(player, this);
        runState = new RunState(player, this);
        Initialize(runState);
    }

    public void Initialize(PlayerState initialState)
    {
        currentState = initialState;
        initialState.OnEnter();
    }

    public void Update()
    {
        currentState.HandleInput();
        currentState.OnUpdate();
    }

    public void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    public void SetState(PlayerState state)
    {
        currentState = nodes[state.GetType()];
        currentState.OnEnter();
    }

    public void ChangeState(PlayerState newState)
    {
        if (newState == currentState) return;

        currentState.OnExit();
        currentState = newState;
        newState.OnEnter();
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
