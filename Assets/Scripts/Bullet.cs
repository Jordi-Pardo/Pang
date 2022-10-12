using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private int _dir = 0;

    public void Destruct()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        Destroy(this.gameObject,2f);
        _dir = Random.Range(0, 2);
    }


    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * bulletSpeed;
        Vector3 newPosition = transform.position;

        if(_dir == 0)
        {
            newPosition.x += Time.deltaTime * 0.5f;
        }
        else
        {
            newPosition.x -= Time.deltaTime * 0.5f;
        }

        transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out IDestructible destructible))
        {
            destructible.Destruct();
            Destroy(gameObject);
        }

        if (collision.transform.CompareTag("Rooftop"))
        {
            Destroy(gameObject);
        }
    }


}
