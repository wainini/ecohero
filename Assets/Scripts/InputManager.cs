using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public Action OnInteractInput;
    public Action OnDropInput;

    private Vector2 moveInput;
    public Vector2 MoveInput { get { return moveInput; } }

    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion

        moveInput = new();
    }

    private void Update()
    {
        ReadMoveInput();
        ReadInteractInput();
        ReadDropInput();
    }

    private void ReadMoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveInput.x = x;
        moveInput.y = y;
    }

    private void ReadInteractInput()
    {
        if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnInteractInput?.Invoke();
        }
    }

    private void ReadDropInput()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            OnDropInput?.Invoke(); 
        }
    }
}
