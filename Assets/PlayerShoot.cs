using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Hook hook;
    [SerializeField] private Transform shootPoint;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Shoot();
        }
    }

    public void Shoot()
    {
        hook.gameObject.SetActive(true);
        hook.gameObject.transform.position = shootPoint.position;
    }
}
