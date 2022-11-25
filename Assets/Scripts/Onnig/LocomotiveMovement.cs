using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Onnig
{
    public class LocomotiveMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1.0f;
        [SerializeField] private float _rotationSpeed = 1.0f;
        [SerializeField] private float _mouseMinDistance = 1.0f; // value will be squared on start
        [SerializeField] private float _minMoveSpeed = 1.0f;
        [SerializeField] private float _minRotationSpeed = 1.0f;

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
            //_mouseMinDistance *= _mouseMinDistance;

            // set random starting direction in 2D space
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            _direction = new Vector3(randomDirection.x, 0, randomDirection.y);

            // set train transform
            transform.position = new Vector3(0, _offsetPositionY, 0);
            transform.LookAt(transform.position + _direction); // TODO : use function instead? (on change)
        }

        private void FixedUpdate()
        {
            float fixedDeltaTime = Time.fixedDeltaTime;

            // move train locomotive
            //transform.position += _direction * (Time.fixedDeltaTime * _speed);

            // TODO : mouse follow
            // - mouse position
            Vector3 mousePosition = GetMousePosition();
            Vector3 vectorTrainToMouse = mousePosition - transform.position;
            //Vector3 mouseDirection = vectorTrainToMouse.normalized;
            float mouseDistance = vectorTrainToMouse.magnitude;

            float trainMoveSpeed = _moveSpeed;
            float trainRotationSpeed = _rotationSpeed;
            // adjust speeds if mouse is too close to train
            if (mouseDistance < _mouseMinDistance)
            {
                float slowRatio = mouseDistance / _mouseMinDistance;
                trainMoveSpeed = Mathf.Lerp(_minMoveSpeed, trainMoveSpeed, slowRatio);
                trainRotationSpeed = Mathf.Lerp(_minRotationSpeed, trainRotationSpeed, slowRatio);
            }

            // - translation and rotation
            Vector3 forwardTrain = transform.forward;
            Vector3 rotationTrain = Vector3.RotateTowards(forwardTrain, /*mouseDirection*/ vectorTrainToMouse, trainRotationSpeed * fixedDeltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(rotationTrain);
            transform.position += forwardTrain * (fixedDeltaTime * trainMoveSpeed); // move forward

            // - ? threshold for mouse input, as in too close stops or slows down
            // - ? click to accelerate
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