using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour, IDestructible
{
    public bool isDestrucible;

    public void Destruct()
    {
        if (isDestrucible)
        {
            Destroy(gameObject);
        }

    }

}
