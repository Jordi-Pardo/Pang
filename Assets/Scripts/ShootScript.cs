using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    [SerializeField] private Transform hookPrefab;

    public void ShootHook()
    {
        Instantiate(hookPrefab);
    }
}
