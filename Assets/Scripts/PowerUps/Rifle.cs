using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : PowerUp
{

    public override void Activate(GameObject gameObject, Action actio,Transform transform)
    {
        if(transform.TryGetComponent(out PlayerShoot playerShoot))
        {
            playerShoot.PickUpRifle();
        }
    }
}
