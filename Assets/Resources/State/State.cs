using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected object ContextObject;
    public State(object Object)
    {
        ContextObject = Object;
    }
    public virtual void Enter() { }
    public virtual void HandleInput() { }
    public virtual void MouseInput() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void Exit() { }
}
