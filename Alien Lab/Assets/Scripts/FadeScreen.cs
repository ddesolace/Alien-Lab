using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [Header("Time Settings")]
    public float FadeTime = 2f;
    public float TextSpawnTime = 1f;
    public float TextScreenTime = 8f;

    [Header("References")]
    public Image _image;
    public Text _text;

    private float _spawnTime;
    private float _screenFadeTime;
    private float _textSpawnTime;
    private float _textFadeTime;

    void Start()
    {
        _screenFadeTime = FadeTime;
        _spawnTime = TextSpawnTime;
        _textSpawnTime = FadeTime;
        _textFadeTime = FadeTime;
    }


    void Update()
    {
        _spawnTime -= Time.deltaTime;
        _screenFadeTime -= Time.deltaTime;

        if (_image != null)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _screenFadeTime);
        }

        if (_spawnTime <= FadeTime / 2)
        {
            if (_text != null)
            {
                _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, -_textSpawnTime);
            }
        }

        if (TextScreenTime <= 0f)
        {
            _textFadeTime -= Time.deltaTime;
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _textFadeTime);
        }

        if (_spawnTime <= 0)
        {
            _textSpawnTime -= Time.deltaTime;
        }

        if (_textSpawnTime <= 0f)
        {
            TextScreenTime -= Time.deltaTime;
        }

    }
}
