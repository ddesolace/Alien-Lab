using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody _rb;
    private PlayerInput _controller;

    private Vector2 _input;
    private Vector2 _direction;

    [SerializeField] private Transform _camera;

    private float _xRotation, _yRotation;

    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _sensitivity = 8f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _controller = new PlayerInput();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _input = _controller.Player.Movement.ReadValue<Vector2>();
        _direction = _controller.Player.Camera.ReadValue<Vector2>();
        
        Camera();

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Camera()
    {

        float mouseX = _direction.x * _sensitivity * Time.deltaTime;
        float mouseY = _direction.y * _sensitivity * Time.deltaTime;

        _yRotation += mouseX;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

        _camera.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        transform.localRotation = Quaternion.Euler(0f, _yRotation, 0f);

    }

    private void Move()
    {
        Vector3 input = (_input.y * _speed * transform.forward) + (_input.x * _speed * transform.right);
        _rb.velocity = new Vector3(input.x, _rb.velocity.y, input.z);
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
