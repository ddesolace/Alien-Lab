using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public float _finalVolume = 0.15f, modifier;

    public AudioSource _music;

    void Start()
    {
        _music.volume = 0f;
    }

    void Update()
    {
        if (_music.volume <= _finalVolume)
        {
            _music.volume += modifier;
        }
    }
}
