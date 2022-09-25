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
        CharacterPlayer PlayerContext = (ContextObject as CharacterPlayer);
        PlayerContext.StopShooting();
        //PlayerContext.PlayAnimation("Dead");
    }
    public override void Die(){}
    public override void IEnter(ICharacterPlayerVisitor playerVisitor){}

    public override void IExit(ICharacterPlayerVisitor playerVisitor){}
    public override void MoveObjectCommand(Vector2 NewPos){}
}
