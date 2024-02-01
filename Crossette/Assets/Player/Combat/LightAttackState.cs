using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttackState : CrossetteState
{
    public LightAttackState(CrossetteStateMachine stateMachine, Player player, Animator animator) : base(stateMachine, player, animator)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("LIGHT ATTACK");
        sm.isAttacking = false;
    }

    public override void OnUpdate()
    {
        if (!sm.isAttacking && sm.timeSinceAttack > 0.25f)
        {
            sm.isAttacking = true;
            sm.currentAttack++;

            sm.hitbox.setResponder(player);
            sm.hitbox.startCheckingCollision();

            // Loop back to one after third attack
            if (sm.currentAttack > 3)
                sm.currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (sm.timeSinceAttack > 1.0f)
                sm.currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            animator.SetTrigger("Attack" + sm.currentAttack);

            // Reset timer
            sm.timeSinceAttack = 0.0f;
            sm.StartCoroutine(sm.WaitForAttackToFinish());
        }
    }

    public override void OnExit()
    {
        Debug.Log("EXIT");
    }
}
