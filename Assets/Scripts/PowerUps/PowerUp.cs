using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour, IPowerUp, IDestructible
{
    [SerializeField] private GameObject bubbleVisual;
    [SerializeField] private Transform visualContainer;
    private SphereCollider _sphereCollider;

    private bool destroyActivated = false;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        visualContainer.Rotate(Vector3.up, 90f * Time.deltaTime);
    }

    public abstract void Activate(GameObject gameObject,Action action,Transform transform);
    public void Destruct()
    {
        bubbleVisual.SetActive(false);
        _sphereCollider.radius = 0.35f;
        GetComponent<Rigidbody>().useGravity = true;
        gameObject.layer = LayerMask.NameToLayer("Bubble");
    }

    public void OnSpawn()
    {
        Destruct();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            if (!destroyActivated)
            {
                Destroy(gameObject, 5f);
                destroyActivated = true;
            }
        }

    }

}
