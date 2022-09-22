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
        AngryNPCContext.PlayAnimation("AngryNPCIdle");
    }
}
