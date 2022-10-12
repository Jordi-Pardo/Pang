using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IDestructible
{
    private Rigidbody _rigidBody;
    [SerializeField] private Ball ballPrefab;

    [SerializeField] private int ballScore;

    public static event Action<int,Ball> OnBallDestroyed;

    public bool IsInstantiated { get; set; }

    public Vector3 initalForce;
    private Vector3 lastVelocity;
    public bool startedFreezed = false;

    private void Awake()
    {
        IsInstantiated = false;
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {

        if (IsInstantiated)
        {
            if (GameManager.instance.Freezed)
            {
                Freeze();
            }
            return;
        }

        AddForceDirection(new Vector3(-4,4,0));
    }

    public void AddForceDirection(Vector3 direction)
    {
        _rigidBody.AddForce(direction, ForceMode.Impulse);
    }

    public void Destruct()
    {
        if (ballPrefab != null)
        {
            SpawnBall(new Vector3(-4, 4, 0));
            SpawnBall(new Vector3(4, 4, 0));

        }

        OnBallDestroyed?.Invoke(ballScore,this);

        Destroy(gameObject);
    }

    private void SpawnBall(Vector3 force)
    {
        Vector3 addOffset = Vector3.zero;
        if (GameManager.instance.Freezed)
        {
            if(force.x > 0)
            {
                addOffset = new Vector3(0.2f, 0, 0);
            }
            else
            {
                addOffset = new Vector3(-0.2f, 0, 0);
            }
        }

        Ball ballMovement = Instantiate(ballPrefab, transform.position + addOffset, Quaternion.identity, transform.parent);
        if (!GameManager.instance.Freezed)
        {
            ballMovement.AddForceDirection(force);
        }
        ballMovement.initalForce = force;
        ballMovement.IsInstantiated = true;
        GameManager.instance.AddBall(ballMovement);
    }

    public void Freeze()
    {
        lastVelocity = _rigidBody.velocity;
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.useGravity = false;
    }

    public void Resume()
    {
        _rigidBody.useGravity = true;
        _rigidBody.velocity = lastVelocity;
    }
    
    public void ResumeFromFreezed()
    {
        AddForceDirection(initalForce);
        _rigidBody.useGravity = true;
        _rigidBody.velocity = lastVelocity;
    }

}
