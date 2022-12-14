using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryNPCState : State
{
    public AngryNPCState(CharacterAngryNPC Object) : base(Object)
    {
    }
    public virtual void CommandToFollow(Character character)
    {
        CharacterAngryNPC AngryNPCContext = (ContextObject as CharacterAngryNPC);
        AngryNPCContext.FollowTarget(character);
    }
}
