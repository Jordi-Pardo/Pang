using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUp
{
    public override void Activate(GameObject gameObject,Action callback)
    {
        gameObject.SetActive(true);
    }
}
