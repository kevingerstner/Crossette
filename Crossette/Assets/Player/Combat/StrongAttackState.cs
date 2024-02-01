using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongAttackState : CrossetteState
{
    public StrongAttackState(CrossetteStateMachine stateMachine, Player player, Animator animator) : base(stateMachine, player, animator)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("STRONG ATTACK");
    }

    public override void OnExit()
    {
        // no operation
    }

    public override void OnUpdate()
    {
        sm.ChangeState(sm.idle);
    }
}
