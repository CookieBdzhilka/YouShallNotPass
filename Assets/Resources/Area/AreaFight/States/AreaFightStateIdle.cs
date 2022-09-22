using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaFightStateIdle : AreaFightState
{
    public AreaFightStateIdle(AreaFight Object) : base(Object)
    {
    }

    public override void CurrentTask()
    {
        AreaFight areaFight = ContextObject as AreaFight;
        areaFight.CommandCalm?.Invoke();
    }
}
