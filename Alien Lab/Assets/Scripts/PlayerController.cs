using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody _rb;
    private PlayerInput _controller;
    private CapsuleCollider _collider;
    private Vector2 _input, _direction, _rotation;

    [Header("Movement Settings")]
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _sensitivity = 8f;
    [SerializeField] private Transform _camera;

    [Header("Jump Settings")]
    [SerializeField] private float _jumpHeight = 10f;
    [SerializeField] private float _jumpBuffer = 0.1f;
    private float _jumpBufferTime;
    private bool _canJump;

    [Header("Head Bob Settings")]
    [SerializeField] private bool _useHeadBob;
    [SerializeField] private float _bobSpeed = 10f;
    [SerializeField] private float _bobAmount = 0.05f;
    [SerializeField] private Transform _head;
    private float _bobTimer, _defaultYPos;

    [Header("Checks")]
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _groundCheckRadius = 0.25f;
    [SerializeField] private float _checkForWallDistance = 2f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _controller = new PlayerInput();
        _collider = GetComponent<CapsuleCollider>();

        _defaultYPos = _head.transform.localPosition.y;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        _controller.Player.Jump.performed += ctx => _jumpBufferTime = _jumpBuffer;
    }

    private void Update()
    {
        _input = _controller.Player.Movement.ReadValue<Vector2>();
        _direction = _controller.Player.Camera.ReadValue<Vector2>();
        
        Camera();
        JumpBuffer();
        HeadBob();
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
    }

    private void Jump()
    {
        // jumping //
        if (IsGrounded() && _canJump)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _jumpHeight, _rb.velocity.z);
            _jumpBufferTime = 0f;
        }

        // reset bools when timer is finished //
        if (_jumpBufferTime <= 0f)
        {
            _canJump = false;
        }
    }

    private void JumpBuffer()
    {
        if (IsGrounded() && _jumpBufferTime > 0f)
        {
            _canJump = true;
        }
        else if (_jumpBufferTime > -0.5f)
        {
            _jumpBufferTime -= Time.deltaTime;
        }
    }

    private void HeadBob()
    {
        if (_input == Vector2.zero || !_useHeadBob || !IsGrounded()) return;

        if (Mathf.Abs(_rb.velocity.x) != 0f || Mathf.Abs(_rb.velocity.z) != 0f)
        {
            _bobTimer += Time.deltaTime * _bobSpeed;
            _head.transform.localPosition = new Vector3(_head.transform.localPosition.x, _defaultYPos + Mathf.Sin(_bobTimer) * _bobAmount, _head.transform.localPosition.z);
        }
        else _head.transform.localPosition = new Vector3(_head.transform.localPosition.x, _defaultYPos, _head.transform.localPosition.z);
    }

    private void Camera()
    {
        float mouseX = _direction.x * _sensitivity * Time.deltaTime;
        float mouseY = _direction.y * _sensitivity * Time.deltaTime;

        _rotation.y += mouseX;
        _rotation.x -= mouseY;
        _rotation.x = Mathf.Clamp(_rotation.x, -80f, 80f);

        _camera.localRotation = Quaternion.Euler(_rotation.x, _rotation.y, 0f);
        transform.localRotation = Quaternion.Euler(0f, _rotation.y, 0f);
    }

    private void Movement()
    {
        Vector3 input = (_input.y * _speed * transform.forward) + (_input.x * _speed * transform.right);
        _rb.velocity = new Vector3(input.x, _rb.velocity.y, input.z);
    }

    public bool IsGrounded()
    {
        Vector3 checkPos = transform.position - new Vector3(0f, _collider.height / 2f) + _collider.center;
        return Physics.CheckSphere(checkPos, _groundCheckRadius, _whatIsGround);
    }

    private void OnEnable()
    {
        _controller.Enable();
    }

    private void OnDisable()
    {
        _controller.Disable();
    }
}
