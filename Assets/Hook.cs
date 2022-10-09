using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private float hookSpeed = 8f;

    MeshCollider meshCollider;
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * hookSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out IDestructible destructible))
        {
            destructible.Destruct();
            gameObject.SetActive(false);
        }

        if (collision.transform.CompareTag("Rooftop"))
        {
            gameObject.SetActive(false);
        }
    }

}
