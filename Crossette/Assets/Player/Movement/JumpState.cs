using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementState
{
    public JumpState(Player player, MovementStateMachine stateMachine, Animator animator) : base(player, stateMachine, animator)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("START JUMP");
        animator.SetTrigger("Jump");
        sm.grounded = false;
        animator.SetBool("Grounded", false);
        player.m_body2d.velocity = new Vector2(player.m_body2d.velocity.x, player.m_jumpForce);
        sm.m_groundSensor.Disable(0.2f);
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
       
    }
}
