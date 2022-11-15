using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Onnig
{
    public class LocomotiveMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 1.0f;

        private Camera _camera;
        private Vector3 _direction;
        private Vector3 _dragStart;
        private Vector3 _dragEnd;
        private float _offsetPositionY; // distance from center of train to game board

        private void Start()
        {
            // init
            _camera = Camera.main;
            _offsetPositionY = transform.lossyScale.y / 2.0f;

            // set random starting direction in 2D space
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            _direction = new Vector3(randomDirection.x, 0, randomDirection.y);

            // set train transform
            transform.position = new Vector3(0, _offsetPositionY, 0);
            transform.LookAt(transform.position + _direction); // TODO : use function instead? (on change)
        }

        private void FixedUpdate()
        {
            // move train locomotive
            transform.position += _direction * (Time.fixedDeltaTime * _speed);
        }

        public void OnTeleportInputed(CallbackContext teleportInput)
        {
            // TODO : set safeguard for when game isn't in progress (ex: menu open, game paused, etc)

            if (teleportInput.started == true)
            {
                _dragStart = GetMousePosition();
            }
            else if (teleportInput.canceled == true)
            {
                _dragEnd = GetMousePosition();
                if (_dragEnd - _dragStart != Vector3.zero)
                {
                    transform.position = _dragStart;
                    _direction = (_dragEnd - _dragStart).normalized;
                    transform.LookAt(transform.position + _direction);
                }

                // TODO : initiate teleport related sequences
            }
        }

        Vector3 GetMousePosition()
        {
            Vector3 mousePos = Mouse.current.position.ReadValue(); // screen position
            mousePos = _camera.ScreenToWorldPoint(mousePos); // world position
            mousePos.y = _offsetPositionY;
            return mousePos;
        }
    }
}