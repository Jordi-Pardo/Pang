using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatch : PowerUp
{
    public override void Activate(GameObject gameObject, Action action, Transform transform)
    {
        if (!GameManager.instance.Freezed)
        {
            action?.Invoke();
        }
    }
}
