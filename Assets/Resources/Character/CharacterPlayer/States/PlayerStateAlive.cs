using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAlive : PlayerState
{
    public PlayerStateAlive(CharacterPlayer Object) : base(Object)
    {
    }

    public override void Die()
    {
        CharacterPlayer cp = ContextObject as CharacterPlayer;
        cp.stateMachine.ChangeState(new PlayerStateDead(cp));
        cp.OnDeadEvent?.Invoke();
    }

    public override void IEnter(ICharacterPlayerVisitor playerVisitor)
    {
        playerVisitor.CharacterPlaerEnter(ContextObject as CharacterPlayer);
    }

    public override void IExit(ICharacterPlayerVisitor playerVisitor)
    {
        playerVisitor.CharacterPlayerExit(ContextObject as CharacterPlayer);
    }

}
