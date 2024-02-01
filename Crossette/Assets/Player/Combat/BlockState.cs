using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : CrossetteState
{
    public BlockState(CrossetteStateMachine stateMachine, Player player, Animator animator) : base(stateMachine, player, animator)
    {
    }

    public override void OnEnter()
    {
        animator.SetTrigger("Block");
        animator.SetBool("IdleBlock", true);

        sm.blockTimer = 0.0f;

        player.movementSM.DisableMovement();
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        animator.SetBool("IdleBlock", false);
        player.movementSM.EnableMovement();

        sm.blockTimer = 0.0f;

    }
}
