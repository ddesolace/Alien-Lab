using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    private PlayerInput _input;
    public Dialogue alien;

    [SerializeField] private Camera _camera;
    [SerializeField] private float _viewDst = 5f;
    [SerializeField] private float _radius = 0.25f;

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

        if (Physics.SphereCast(_camera.transform.position, _radius, _camera.transform.forward, out RaycastHit hit, _viewDst))
        {
            if (hit.transform.gameObject.TryGetComponent<Dialogue>(out Dialogue info))
            {
                alien = info;
                alien._on = true;
                print(hit.transform.gameObject.name);
            }
            else
            {
                if (alien != null)
                    alien._on = false;
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
