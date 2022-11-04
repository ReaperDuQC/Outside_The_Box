using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kevin
{
    public class MiniGameManager : MonoBehaviour
    {
        [SerializeField] TrainController _trainController;
        Controls _controls;
        [SerializeField] Vector2 _movementInput;
        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.TrainController.Move.performed += i => _movementInput = i.ReadValue<Vector2>();
                _controls.TrainController.Move.canceled += i => _movementInput = i.ReadValue<Vector2>();
            }
            _controls.Enable();
        }
        private void OnDisable()
        {
            _controls.Disable();
        }
        private void Update()
        {
            HandleMovementInput();
            HandleRotationInput();
        }
        void HandleMovementInput()
        {
            if (Mathf.Abs(_movementInput.y) > 0)
            {
                _trainController.Accelerate(_movementInput.y);
            }
        }
        void HandleRotationInput()
        {
            if (Mathf.Abs(_movementInput.x) > 0)
            {
                _trainController.Turn(_movementInput.x);
            }
        }
    }
}