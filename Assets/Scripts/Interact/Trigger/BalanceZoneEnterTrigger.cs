using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceZoneEnterTrigger : Trigger {

    public override void ExecuteTrigger()
    {
        p.Balancing = true;
    }
}
