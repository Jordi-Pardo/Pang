using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Hook hook;
    [SerializeField] private Transform shootPoint;
    public bool IsShooting { get; set; }
    public void Shoot()
    {
        if (hook.isActiveAndEnabled)
            return;
        IsShooting = true;
        hook.gameObject.SetActive(true);
        hook.gameObject.transform.position = shootPoint.position;
    }

    public Hook GetHook() => hook;
}
