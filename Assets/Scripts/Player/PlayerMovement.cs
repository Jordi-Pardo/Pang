using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerMovement : MonoBehaviour
{


    private const float rotationTime = 0.3f;

    [SerializeField] private Animator animator;

    private PlayerShoot _playerShoot;

    private bool _isRunning;

    private float _moveSpeed = 5f;

    private float screenLimitOffset = 0.03f;

    private void Awake()
    {
        _playerShoot = GetComponent<PlayerShoot>();
    }

    void Update()
    {
        if (!GameManager.instance.started)
            return;

        HandleMovement();
    }

    private void HandleMovement()
    {
        if (_playerShoot.IsShooting)
            return;

        _playerShoot.AimToRun();
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





}
