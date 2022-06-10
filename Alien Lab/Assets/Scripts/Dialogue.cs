using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{

    public bool _on;

    public GameObject _Canvas;

    void Start()
    {
        _Canvas.SetActive(false);
        _on = false;
    }

    void Update()
    {
        if (_on)
        {
            _Canvas.SetActive(true);
        }
        else
        {
            _Canvas.SetActive(false);
        }
    }
}
