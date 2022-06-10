using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    private PlayerInput _input;

    public bool _on;
    public GameObject _Canvas;
    public GameObject _Text;
    public Text _image;

    public float ScreenTime = 5f, SpawnSpeed = 2f;

    [TextArea(3,10)]
    public string[] dialogueText;

    private float _timer, _screenTimer, _spawnTimer;
    private readonly float _maxTime = 2f;
    private bool _delay, spawn, show;
    private int number = 0;
    
    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.Interact.performed += ctx => ShowDialogue();
    }

    void Start()
    {
        _Canvas.SetActive(false);

        _image.text = dialogueText[0];
        show = true;
        ResetValues();
    }

    void Update()
    {
        if (_on && show)
        {
            _Canvas.SetActive(true);
        }
        else _Canvas.SetActive(false);

        if (spawn)
        {
            _spawnTimer -= Time.deltaTime * SpawnSpeed;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, -_spawnTimer);
        }

        if (_delay) _screenTimer -= Time.deltaTime;

        if (_screenTimer <= 0f)
        {
            _timer -= Time.deltaTime;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _timer);
        }
        if (_timer < 0f)
        {
            show = true;

            if (number >= dialogueText.Length - 1)
            {
                number = 0;
            }
            else number++;

            _image.text = dialogueText[number];
            ResetValues();
        }
    }

    private void ShowDialogue()
    {
        if (!_on) return;

        show = false;
        spawn = true;
        _delay = true;
    }

    private void ResetValues()
    {
        _on = false;
        spawn = false;
        _delay = false;

        _timer = _maxTime;
        _spawnTimer = _maxTime;
        _screenTimer = ScreenTime;
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
