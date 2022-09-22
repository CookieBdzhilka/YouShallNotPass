using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryNPCStateAttack : AngryNPCState
{
    public AngryNPCStateAttack(CharacterAngryNPC Object) : base(Object)
    {
    }

    public override void Enter()
    {
        CharacterAngryNPC AngryNPCContext = (ContextObject as CharacterAngryNPC);
        AngryNPCContext.PlayAnimation("AngryNPCWalk");
    }

    public override void PhysicsUpdate()
    {
        CharacterAngryNPC characterAngryNPC = ContextObject as CharacterAngryNPC;
        characterAngryNPC.MoveToTarget();
    }
}
