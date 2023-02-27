using System;
using Unity.VisualScripting;
using UnityEngine;

public class DebugPlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float groundDrag;

    [SerializeField] private Transform orientation;

    private float _horizontalInput;
    private float _verticalInput;

    private Vector3 _moveDirection;

    private Rigidbody _rigidbody;
    
    
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        // Stop rigid body from falling over
        _rigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        GetInput();
        _rigidbody.drag = groundDrag;
    }

    private void GetInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
        
        _rigidbody.AddForce(_moveDirection.normalized * (movementSpeed * 10.0f), ForceMode.Force);
    }
}
