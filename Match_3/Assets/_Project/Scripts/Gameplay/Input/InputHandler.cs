using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float _detectonDistance;

    private PlayerInput _playerInput;

    private Vector2 _startPosition;

    private bool _isSwiping;
    private Vector2 _position => _playerInput.PlayerSwipe.Position.ReadValue<Vector2>(); 
    public void Start()
    {
        _playerInput = new PlayerInput();
        _playerInput.PlayerSwipe.Enable();
        _playerInput.PlayerSwipe.Press.performed += StartSwipe;

        _isSwiping = false;
    }

    private void Update()
    {
        if (!_isSwiping) return;

        if(Vector2.Distance(_startPosition, _position) < _detectonDistance)
        {
            Vector2 direction = (_position - _startPosition).normalized;

            if(Vector2.Dot(Vector2.up,direction) > 0.9f)
            {
                _isSwiping = false;
                Debug.Log("Up");
            }
            else if (Vector2.Dot(Vector2.down, direction) > 0.9f)
            {
                _isSwiping = false;
                Debug.Log("Dowm");
            }
            else if (Vector2.Dot(Vector2.right, direction) > 0.9f)
            {
                _isSwiping = false;
                Debug.Log("Right");
            }
            else if (Vector2.Dot(Vector2.left, direction) > 0.9f)
            {
                _isSwiping = false;
                Debug.Log("Left");
            }
        }
    }

    private void StartSwipe(InputAction.CallbackContext context)
    {
         _isSwiping = true;
        _startPosition = _position;
    }
}
