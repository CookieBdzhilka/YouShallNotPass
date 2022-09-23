using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryNPCStateIdle : AngryNPCState
{
    public AngryNPCStateIdle(CharacterAngryNPC Object) : base(Object)
    {
    }

    public override void Enter()
    {
        CharacterAngryNPC AngryNPCContext = (ContextObject as CharacterAngryNPC);
        AngryNPCContext.MeleeArea.Sleep = true;
        AngryNPCContext.PlayAnimation("AngryNPCIdle");
    }

    public override void Exit()
    {
        CharacterAngryNPC AngryNPCContext = (ContextObject as CharacterAngryNPC);
        AngryNPCContext.MeleeArea.Sleep = false;
    }
}
