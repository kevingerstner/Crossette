using System;

// A predicate is a function that evaluates a boolean, used to see if a transition is valid.
public interface IPredicate
{
    bool Evaluate();
}

// A Func Predicate implements IPredicate, for functions
public class FuncPredicate : IPredicate
{
    readonly Func<bool> func;

    public FuncPredicate(Func<bool> func)
    {
        this.func = func;
    }

    public bool Evaluate() => func.Invoke();
}

public interface ITransition
{
    IState To { get; }
    IPredicate Condition { get; }
}

public class Transition : ITransition
{
    public IState To {get;}

    public IPredicate Condition { get; }

    public Transition(IState to, IPredicate condition)
    {
        To = to;
        Condition = condition;
    }
}