using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunState : MovementState
{
    public RunState(Player player, MovementStateMachine stateMachine, Animator animator) : base(player, stateMachine, animator)
    {
    }

    public override void OnEnter()
    {
    }

    public override void OnUpdate()
    {        
        SwapSprite();

        //Run
        if (Mathf.Abs(sm.inputX) > Mathf.Epsilon)
        {
            sm.delayToIdle = 0.05f; // Reset timer

            sm.movement.x = sm.facingDirection;
            animator.SetInteger("AnimState", 1);
        }
        else
        {
            sm.movement.x = 0;
            animator.SetInteger("AnimState", 0);
        }

        //transform.position += m_movement * Time.deltaTime * m_speed;
        player.m_body2d.velocity = new Vector2(sm.movement.x * player.m_speed, player.m_body2d.velocity.y);
    }
    public override void OnExit()
    {
        animator.SetInteger("AnimState", 0);
    }

    public void SwapSprite()
    {
        // Swap direction of sprite depending on walk direction
        if (sm.inputX > 0)
        {
            //GetComponent<SpriteRenderer>().flipX = false;
            player.transform.eulerAngles = new Vector3(0, 0, 0);
            sm.facingDirection = 1;
        }
        else if (sm.inputX < 0)
        {
            //GetComponent<SpriteRenderer>().flipX = true;
            player.transform.eulerAngles = new Vector3(0, 180, 0);
            sm.facingDirection = -1;
        }
    }
}
