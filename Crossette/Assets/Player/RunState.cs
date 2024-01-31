using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunState : PlayerState
{
    public RunState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        SwapSprite();

        //Run
        if (Mathf.Abs(stateMachine.inputX) > Mathf.Epsilon)
        {
            stateMachine.m_delayToIdle = 0.05f; // Reset timer

            stateMachine.m_movement.x = stateMachine.m_facingDirection;
            player.m_animator.SetInteger("AnimState", 1);
        }
        else
        {
            stateMachine.m_movement.x = 0;
            player.m_animator.SetInteger("AnimState", 0);
        }

        //transform.position += m_movement * Time.deltaTime * m_speed;
        player.m_body2d.velocity = new Vector2(stateMachine.m_movement.x * player.m_speed, player.m_body2d.velocity.y);
    }
    public override void OnExit()
    {
        base.OnExit();
        player.m_animator.SetInteger("AnimState", 0);

    }

    public void SwapSprite()
    {
        // Swap direction of sprite depending on walk direction
        if (stateMachine.inputX > 0)
        {
            //GetComponent<SpriteRenderer>().flipX = false;
            player.transform.eulerAngles = new Vector3(0, 0, 0);
            stateMachine.m_facingDirection = 1;
        }
        else if (stateMachine.inputX < 0)
        {
            //GetComponent<SpriteRenderer>().flipX = true;
            player.transform.eulerAngles = new Vector3(0, 180, 0);
            stateMachine.m_facingDirection = -1;
        }
    }
}
