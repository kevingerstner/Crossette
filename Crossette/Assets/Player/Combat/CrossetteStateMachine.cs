using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class CrossetteStateMachine : IStateMachine<CrossetteState>
{
    private Player player;
    private PlayerInput playerInput;
    private Animator animator;
    public Hitbox hitbox;

    public IdleAttackState idle;
    public LightAttackState lightAttack;
    public StrongAttackState strongAttack;
    public BlockState blockState;

    public bool isAttacking = false;
    public float timeSinceAttack = 0.0f;
    public float blockTimer = 0.0f;
    public int currentAttack = 0;

    public override void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        hitbox = GetComponentInChildren<Hitbox>();
        playerInput = GetComponent<PlayerInput>();

        InputAction blockAction = playerInput.actions["Crossette/Block"];
        blockAction.started += OnBlockStart;
        blockAction.canceled += OnBlockCancel;

        idle = new IdleAttackState(this, player, animator);
        lightAttack = new LightAttackState(this, player, animator);
        strongAttack = new StrongAttackState(this, player, animator);
        blockState = new BlockState(this, player, animator);

        Initialize(idle);
    }

    public override void Update()
    {
        base.Update();
        // Increase timer that controls attack combo
        timeSinceAttack += Time.deltaTime;
        blockTimer += Time.deltaTime;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnLightAttack()
    {
        //if (m_killed || m_stunned) return;

        ChangeState(lightAttack);
    }

    private void OnStrongAttack()
    {
        ChangeState(strongAttack);
    }

    private void OnBlockStart(InputAction.CallbackContext context)
    {
        if (blockTimer < player.block_downtime) return;
        Debug.Log("STARTED");
        ChangeState(blockState);
    }


    private void OnBlockCancel(InputAction.CallbackContext context)
    {
        Debug.Log("CANCEL");
        ChangeState(idle);
    }

    private void GetHit()
    {
        //if (m_killed) return;

        animator.SetTrigger("Hurt");
        //m_stunned = true;
        //DisableMovement();
        //Invoke(nameof(Recover), 0.3f);
    }

    public IEnumerator WaitForAttackToFinish()
    {
        yield return new WaitForSeconds(player.attack_dur);

        hitbox.stopCheckingCollision();
        isAttacking = false;
        ChangeState(idle);
    }
}

public abstract class CrossetteState : IState
{
    protected readonly Player player;
    protected readonly CrossetteStateMachine sm;
    protected readonly Animator animator;

    protected CrossetteState(CrossetteStateMachine stateMachine, Player player, Animator animator)
    {
        this.player = player;
        this.sm = stateMachine;
        this.animator = animator;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public virtual void FixedUpdate() { }
    public abstract void OnExit();
}