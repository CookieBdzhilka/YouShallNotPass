using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWalk : PlayerState
{
    public PlayerStateWalk(CharacterPlayer Object) : base(Object)
    {
    }

    public override void Enter()
    {
        CharacterPlayer PlayerContext = (ContextObject as CharacterPlayer);
        PlayerContext.PlayAnimation("Run");
    }
}
