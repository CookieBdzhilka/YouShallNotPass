using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryNPCStateAttacking : AngryNPCState
{
    public AngryNPCStateAttacking(CharacterAngryNPC Object) : base(Object)
    {
    }

    public override void CommandToFollow(Character character)
    {
    }

    public override void Enter()
    {
        CharacterAngryNPC AngryNPCContext = (ContextObject as CharacterAngryNPC);
        AngryNPCContext.PlayAnimation("Attack");
    }
}
