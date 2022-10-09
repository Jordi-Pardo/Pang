using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerMovement : MonoBehaviour
{
    private const float rotationTime = 0.3f;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private Vector3 runAimingTargetPos;
    [SerializeField] private Vector3 shootAimingTargetPos = new Vector3(-0.368000001f, 1.96800005f, -0.493000001f);
    [SerializeField] private MultiAimConstraint bodyAimConstraint;
    [SerializeField] private Animator animator;

    private Rigidbody _rigidbody;
    private bool _isShooting;
    private bool _isRunning;

    private float _moveSpeed = 5f;
    private int _direction = 0;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //transform.forward = Vector3.right;
    }
    // Update is called once per frame
    void Update()
    {
        HandleInputs();

        if (_isShooting)
        {
            aimTarget.localPosition = Vector3.Lerp(aimTarget.localPosition, shootAimingTargetPos, Time.deltaTime * 30f);
        }
        else
        {
            aimTarget.localPosition = Vector3.Lerp(aimTarget.localPosition, runAimingTargetPos, Time.deltaTime * 30f);

        }

        //if (_isRunning && !_isShooting)
        //{

        //    transform.position += _moveSpeed * Time.deltaTime * (_direction == 1 ? Vector3.right : Vector3.left);
        //}
 
    }

    private void HandleInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {

            bodyAimConstraint.weight = 1;
            _isShooting = true;
            animator.SetBool("IsShooting", _isShooting);

        }

        if (Input.GetMouseButtonUp(0))
        {
            bodyAimConstraint.weight = 0;
            _isShooting = false;
            animator.SetBool("IsShooting", _isShooting);
        }

        float x = Input.GetAxisRaw("Horizontal");
        _isRunning = x != 0;
        animator.SetBool("IsRunning", _isRunning);

        if (_isShooting)
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


        //if (Input.GetKey(KeyCode.D))
        //{
        //    _isRunning = true;
        //    _direction = 1;
        //    animator.SetBool("IsRunning",_isRunning);
        //    iTween.RotateTo(gameObject, new Vector3(0, 90f, 0), rotationTime);
        //}
        //else if (Input.GetKeyUp(KeyCode.D))
        //{
        //    _isRunning = false;
        //    animator.SetBool("IsRunning",_isRunning);
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    _direction = -1;
        //    _isRunning = true;
        //    animator.SetBool("IsRunning", _isRunning);
        //    iTween.RotateTo(gameObject, new Vector3(0, 270f, 0), rotationTime);
        //}
        //else if (Input.GetKeyUp(KeyCode.A))
        //{
        //    _isRunning = false;
        //    animator.SetBool("IsRunning", _isRunning);
        //}

    }

    private void HandleRotation()
    {
        
    }
}
