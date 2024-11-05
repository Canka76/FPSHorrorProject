using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBreathScript : BreatheAction
{
    public override void OnFocusEnemy()
    {
       print("looking at enemy" + gameObject.name);
    }

    public override void OnLoseFocusEnemy()
    {
        print("Stopped looking at enemy" + gameObject.name);

    }
}
