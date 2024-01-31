using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(Player player, StateMachine stateMachine) : base(player, stateMachine){}

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

}
