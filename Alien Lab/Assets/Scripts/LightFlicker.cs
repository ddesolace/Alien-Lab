using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Header("Ranndomize Settings")]
    public float HowManySides;
    public int Threshold;

    [Header("Min and Max Settings")]
    public Vector2 longTimer;
    public Vector2 shortTimer;

    [Header("References")]
    public GameObject[] _lights;

    private float timer, number;
    private Vector2 currentTimer;
    private bool _switch;

    void Start()
    {
        timer = Random.Range(longTimer.x, longTimer.y);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (!_switch)
            {
                number = Random.Range(1, HowManySides);
                if (number < Threshold)
                {
                    currentTimer = shortTimer;
                }
                else currentTimer = longTimer;

                foreach (var light in _lights)
                {
                    light.SetActive(false);
                }
                _switch = true;
                timer = Random.Range(currentTimer.x, currentTimer.y);
            }
            else
            {
                foreach (var light in _lights)
                {
                    light.SetActive(true);
                }
                _switch = false;
                timer = Random.Range(currentTimer.x, currentTimer.y);
            }
        }
    }
}
