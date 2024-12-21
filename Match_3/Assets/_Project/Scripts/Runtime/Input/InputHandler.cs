using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public event Action<Vector2Int> onSwipe;
    public event Action<Vector2> startSwipe;

    [SerializeField] private float _detectonDistance;

    private PlayerInput _playerInput;
    private Vector2 _startPosition;

    private bool _isSwiping;
    private Vector2 _position => _playerInput.PlayerSwipe.Position.ReadValue<Vector2>(); 
    public void Init()
    {
        _playerInput = new PlayerInput();
        _playerInput.PlayerSwipe.Press.performed += StartSwipe;

        _isSwiping = false;
    }

    private void OnDisable() => _playerInput.PlayerSwipe.Disable();

    public void EnableInput() => _playerInput.PlayerSwipe.Enable();
    public void DisableInput() => _playerInput.PlayerSwipe.Disable();
    private void Update()
    {
        if (!_isSwiping) return;

        DetectSwipeDirection();
    }

    private void StartSwipe(InputAction.CallbackContext context)
    {
        _isSwiping = true;
        _startPosition = _position;
        startSwipe?.Invoke(_startPosition);
    }

    private void DetectSwipeDirection()
    {
        if (Vector2.Distance(_startPosition, _position) > _detectonDistance)
        {
            Vector2 direction = (_position - _startPosition).normalized;

            if (Vector2.Dot(Vector2.up, direction) > 0.9f)
            {
                _isSwiping = false;
                onSwipe?.Invoke(Vector2Int.up);
            }
            else if (Vector2.Dot(Vector2.down, direction) > 0.9f)
            {
                _isSwiping = false;
                onSwipe?.Invoke(Vector2Int.down);

            }
            else if (Vector2.Dot(Vector2.right, direction) > 0.9f)
            {
                _isSwiping = false;
                onSwipe?.Invoke(Vector2Int.right);
            }
            else if (Vector2.Dot(Vector2.left, direction) > 0.9f)
            {
                _isSwiping = false;
                onSwipe?.Invoke(Vector2Int.left);
            }
        }
    }
}