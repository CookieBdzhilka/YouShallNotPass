using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDead : PlayerState
{
    public PlayerStateDead(CharacterPlayer Object) : base(Object)
    {
    }

    public override void Enter()
    {
        CharacterPlayer cp = (ContextObject as CharacterPlayer);
        cp.StopShooting();
    }

    public override void MoveObjectCommand(Vector2 NewPos)
    {
    }
}
