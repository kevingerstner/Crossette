using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementState
{
    public IdleState(Player player, MovementStateMachine stateMachine, Animator animator) : base(player, stateMachine, animator){}

    public override void FixedUpdate()
    {
    }

    public override void OnEnter()
    {
        sm.movement = Vector3.zero;
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        
    }

}
