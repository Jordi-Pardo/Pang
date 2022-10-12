using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePowerUp : MonoBehaviour,IDestructible
{
    public void Destruct()
    {
        if(transform.parent.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.useGravity = true;
        }
        Destroy(gameObject);
    }
}
