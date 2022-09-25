using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAttack : PlayerState
{
    public PlayerStateAttack(CharacterPlayer Object) : base(Object)
    {
    }

    public override void Enter()
    {
        CharacterPlayer PlayerContext = (ContextObject as CharacterPlayer);
        PlayerContext.PlayAnimation("Attack");
    }
}
