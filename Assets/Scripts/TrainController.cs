using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kevin
{
    public class TrainController : MonoBehaviour
    {
        [SerializeField] float _currentspeed;
        [SerializeField] float _accelerationFactor = 0.5f;
        [SerializeField] float _minMoveSpeed = -5f;
        [SerializeField] float _maxMoveSpeed = 100f;
        [SerializeField] Vector3 _rotationDirection;
        [SerializeField] float _rotationSpeed = 5f;
        [SerializeField] Quaternion _currentRotation;

        [SerializeField] GameObject _engine;
        [SerializeField] List<GameObject> _wagons;

        [SerializeField] bool _debug;
        [SerializeField] float _debugRayDistance = 5f;

        private void OnDrawGizmos()
        {
            if (!_debug) return;
            if (_engine == null) return;

            Gizmos.color = Color.green;
            Gizmos.DrawRay(_engine.transform.position, _engine.transform.up * _debugRayDistance);
        }
        public void Accelerate(float accelerationCoefficient)
        {
            _currentspeed = Mathf.Clamp(_currentspeed + (accelerationCoefficient * _accelerationFactor), _minMoveSpeed, _maxMoveSpeed);
        }
        public void Turn(float angle)
        {
            _currentRotation = _engine.transform.rotation;
            Quaternion desiredRotation = _currentRotation * Quaternion.Euler(angle * _rotationDirection * _rotationSpeed);
            _engine.transform.rotation = Quaternion.Slerp(_currentRotation, desiredRotation, Time.deltaTime);
        }
        private void Update()
        {
            _engine.transform.position = Vector3.Lerp(_engine.transform.position, _engine.transform.position + _engine.transform.up * _currentspeed, Time.deltaTime);
        }

        public void AddWagon()
        {

        }
    }
}