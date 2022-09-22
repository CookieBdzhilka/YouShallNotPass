using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
    public PlayerState(CharacterPlayer Object) : base(Object)
    {
        ContextObject = Object;
    }
    public virtual void IEnter(ICharacterPlayerVisitor playerVisitor)
    {

    }
    public virtual void IExit(ICharacterPlayerVisitor playerVisitor)
    {

    }
    public virtual void MoveObjectCommand(Vector2 NewPos)
    {
        CharacterPlayer cp = (ContextObject as CharacterPlayer);
        cp.Move(NewPos);
    }
    public virtual void Die()
    {

    }
}
