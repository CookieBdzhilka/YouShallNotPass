using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaFightStateWar : AreaFightState
{
    public AreaFightStateWar(AreaFight Object) : base(Object)
    {
    }

    public override void CurrentTask()
    {
        AreaFight areaFight = ContextObject as AreaFight;
        areaFight.AttackCommand();
    }
}
