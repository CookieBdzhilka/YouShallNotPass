using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : PlayerState
{
    public PlayerStateIdle(CharacterPlayer Object) : base(Object)
    {
    }

    public override void Enter()
    {
        CharacterPlayer PlayerContext = (ContextObject as CharacterPlayer);
        PlayerContext.PlayAnimation("Idle");
    }
}
