using UnityEngine;
using System;
using System.Collections.Generic;

public interface IState
{
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnUpdate();
    public abstract void FixedUpdate();
}

public class PlayerState : IState
{
    protected readonly Player player;
    protected readonly StateMachine stateMachine;

    protected PlayerState(Player player, StateMachine stateMachine) 
    { 
        this.player = player; 
        this.stateMachine = stateMachine; 
    }
    public virtual void OnEnter()
    {
        Debug.Log("Enter State: " + this.ToString());
    }

    public virtual void HandleInput()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void FixedUpdate()
    {
        
    }
    public virtual void OnExit()
    {
        Debug.Log("Exit State: " + this.ToString());
    }
}
