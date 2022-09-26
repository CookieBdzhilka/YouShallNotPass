using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryNPCStateFollow : AngryNPCState
{
    public AngryNPCStateFollow(CharacterAngryNPC Object) : base(Object)
    {
    }

    public override void Enter()
    {
        CharacterAngryNPC AngryNPCContext = (ContextObject as CharacterAngryNPC);
        AngryNPCContext.PlayAnimation("Walk");
    }

    public override void PhysicsUpdate()
    {
        CharacterAngryNPC characterAngryNPC = ContextObject as CharacterAngryNPC;
        characterAngryNPC.MoveToTarget();
    }
    public override void LogicUpdate()
    {
        CharacterAngryNPC characterAngryNPC = ContextObject as CharacterAngryNPC;
        if(Vector3.Distance(characterAngryNPC.transform.position, characterAngryNPC.Target.transform.position) <= characterAngryNPC.AttackDistance)
        {
            characterAngryNPC.StartAttacking();
        }
    }
}
