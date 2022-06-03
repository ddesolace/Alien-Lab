using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{

    private PlayerInput _input;

    [SerializeField] private Camera _camera;
    [SerializeField] private float _viewDst = 5f;

    private void Awake()
    {
        _input = new PlayerInput();
    }


    void Update()
    {
        Interact();
    }

    private void Interact()
    {
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _viewDst, Color.green);

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _viewDst))
        {
            if (hit.transform.gameObject.TryGetComponent<Interacable>(out _))
            {
                print("hit");
            }
        }

    }

    private RaycastHit CastRay()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);

        return hit;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

}
