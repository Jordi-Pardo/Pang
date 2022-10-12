using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerMovement : MonoBehaviour
{
    public static Action OnDead;

    private const float rotationTime = 0.3f;

    [SerializeField] private Transform aimTarget;
    [SerializeField] private Vector3 runAimingTargetPos;
    [SerializeField] private Vector3 shootAimingTargetPos = new Vector3(-0.368000001f, 1.96800005f, -0.493000001f);
    [SerializeField] private MultiAimConstraint bodyAimConstraint;
    [SerializeField] private Animator animator;

    [SerializeField] private PlayerShoot playerShoot;

    private bool _isRunning;

    private float _moveSpeed = 5f;

    private float screenLimitOffset = 0.03f;

    void Update()
    {
        HandleInputs();

        HandleMovement();
    }

    private void HandleMovement()
    {
        if (playerShoot.IsShooting)
            return;

        aimTarget.localPosition = Vector3.Lerp(aimTarget.localPosition, runAimingTargetPos, Time.deltaTime * 30f);

        float x = Input.GetAxisRaw("Horizontal");
        _isRunning = x != 0;
        animator.SetBool("IsRunning", _isRunning);
        if (Camera.main.WorldToViewportPoint(gameObject.transform.position).x > 1 - screenLimitOffset && x > 0 || Camera.main.WorldToViewportPoint(gameObject.transform.position).x < 0 + screenLimitOffset && x < 0)
            return;
        if (x > 0)
        {
            transform.position += _moveSpeed * Time.deltaTime * Vector3.right;
            iTween.RotateTo(gameObject, new Vector3(0, 90f, 0), rotationTime);

        }
        else if (x < 0)
        {
            transform.position += _moveSpeed * Time.deltaTime * Vector3.left;
            iTween.RotateTo(gameObject, new Vector3(0, 270f, 0), rotationTime);
        }
    }

    private void HandleInputs()
    {
        //Can shoot only when hook is disabled and you clicked
        if (Input.GetMouseButtonDown(0) && !playerShoot.GetHook().isActiveAndEnabled)
        {
            StartCoroutine(Shoot());
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);
            OnDead?.Invoke();
        }
    }

    public IEnumerator Shoot()
    {
        bool completed = false;

        playerShoot.Shoot();
        animator.SetBool("IsShooting", playerShoot.IsShooting);
        bodyAimConstraint.weight = 1;


        while (!completed)
        {
            aimTarget.localPosition = Vector3.Lerp(aimTarget.localPosition, shootAimingTargetPos, Time.deltaTime * 30f);
            if (Vector3.Distance(aimTarget.localPosition, shootAimingTargetPos) > 0.05f)
            {
                yield return null;
            }
            else
            {
                completed = true;
            }
        }

        playerShoot.IsShooting = false;
        animator.SetBool("IsShooting", playerShoot.IsShooting);
        bodyAimConstraint.weight = 0;

    }

}
