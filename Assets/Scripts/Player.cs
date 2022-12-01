using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float _gravity=-9.8f;

    [SerializeField]private float _horizontalSpeed=1f;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] private float _jumpSpeed = 5f;
    [SerializeField] private float _groundCheckRadius=0.1f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField]private float _verticalSpeed = 0f;

    [SerializeField]private bool _onGround = false;

    private void Update()
    {
        if (IsOnGround())
        {
            _verticalSpeed = 0;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            Fall();
        }
        MoveForward();
    }

    private void MoveForward()
    {
        transform.Translate(new Vector3(_horizontalSpeed * Time.deltaTime, _verticalSpeed*Time.deltaTime, 0));
    }

    private void Jump()
    {
        _verticalSpeed = _jumpSpeed +Mathf.Sqrt(_jumpHeight / (2 * _gravity * _gravity));
    }

    private void Fall()
    {
        _verticalSpeed += _gravity * Time.deltaTime;
    }

    private bool IsOnGround()
    {
        return _onGround= Physics.OverlapSphere(transform.position, _groundCheckRadius, _groundLayer).Length != 0;
    }

}
