using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryNPCState : State
{
    public AngryNPCState(CharacterAngryNPC Object) : base(Object)
    {
        ContextObject = Object;
    }
}
