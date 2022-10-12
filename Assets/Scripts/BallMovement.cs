using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour, IDestructible
{
    private Rigidbody _rigidBody;
    private Vector3 firstImpact;
    [SerializeField] private BallMovement ballPrefab;

    [SerializeField] private int ballScore;

    public static event Action<int,GameObject> OnUpdateScore;

    public bool IsInstantiated { get; set; }


    private void Awake()
    {
        IsInstantiated = false;
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {

        if (IsInstantiated)
            return;

        AddForceDirection(new Vector3(-4,4,0));
    }

    private void AddForceDirection(Vector3 direction)
    {
        _rigidBody.AddForce(direction, ForceMode.Impulse);
    }

    public void Destruct()
    {
        if (ballPrefab != null)
        {
            BallMovement ballMovement = Instantiate(ballPrefab,transform.position,Quaternion.identity,transform.parent);
            GameManager.instance.AddBall(ballMovement.gameObject);
            ballMovement.IsInstantiated = true;
            ballMovement.AddForceDirection(new Vector3(-4, 4, 0));
            ballMovement = Instantiate(ballPrefab,transform.position,Quaternion.identity,transform.parent);
            GameManager.instance.AddBall(ballMovement.gameObject);
            ballMovement.IsInstantiated = true;
            ballMovement.AddForceDirection(new Vector3(4, 4, 0));
        }

        OnUpdateScore?.Invoke(ballScore,gameObject);

        Destroy(gameObject);
    }

}
