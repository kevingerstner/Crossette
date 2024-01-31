using UnityEngine;

public abstract class IStateMachine<State> : MonoBehaviour where State : IState
{
    public State currentState;

    public abstract void Start();

    public virtual void Initialize(State initialState)
    {
        currentState = initialState;
        initialState.OnEnter();
    }

    public virtual void Update()
    {
        currentState.OnUpdate();
    }

    public virtual void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    public virtual void SetState(State state)
    {
        currentState.OnEnter();
    }

    public virtual void ChangeState(State newState)
    {
        currentState.OnExit();
        currentState = newState;
        newState.OnEnter();
    }
}
