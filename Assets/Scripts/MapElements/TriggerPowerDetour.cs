using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPowerDetour : TriggerBase
{
    [SerializeField] PowerSourceDetour powerSourceDetour;

    public override void DoAction()
    {
        powerSourceDetour.Activate();
        powerSourceDetour.isPowered = true;
    }
}
