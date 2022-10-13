using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject shieldVisual;

    public static Action OnDead;

    public void ToggleShieldVisual()
    {
        bool state = shieldVisual.activeInHierarchy;
        state = !state;
        shieldVisual.SetActive(state);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.CompareTag("Ball"))
        {
            if (shieldVisual.activeInHierarchy)
            {
                if (collision.transform.TryGetComponent(out IDestructible destructible))
                {
                    destructible.Destruct();
                }
                shieldVisual.SetActive(false);
                return;
            }

            Destroy(collision.gameObject);
            OnDead?.Invoke();
        }

        if (collision.transform.TryGetComponent(out IPowerUp powerUp))
        {
            powerUp.Activate(shieldVisual, GameManager.instance.StartStopWatch,transform);
            Destroy(collision.gameObject);
        }
    }

}
