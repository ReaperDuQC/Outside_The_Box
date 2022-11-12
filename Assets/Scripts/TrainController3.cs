using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kevin
{
    struct Wagon
    {
        public Vector2 _currentVelocity;
    }
    struct Locomotive
    {
        public Vector2 _currentVelocity;
    }
    public class TrainController3 : MonoBehaviour
    {
        [SerializeField] float _accelerationFactor = 0.5f;
        [SerializeField] float _minMoveSpeed = 0f;
        [SerializeField] float _maxMoveSpeed = 100f;

        [SerializeField] Vector3 _rotationDirection;
        [SerializeField] float _rotationSpeed = 180f;

        [SerializeField] GameObject _enginePrefab;
        [SerializeField] List<GameObject> _wagonPrefabs;


        [SerializeField] Locomotive _locomotive;    
        [SerializeField] List<Wagon> _train;

        [SerializeField] Transform _engine;
        [SerializeField] List<Transform> _wagons;

        [SerializeField] int _initialNbWagons;
        private void Awake()
        {
            _locomotive = new Locomotive();
            _engine = Instantiate(_enginePrefab).transform;

            for (int i = 0; i < _initialNbWagons; i++)
            {
                AddWagon();
            }
        }
        public void Accelerate(float accelerationCoefficient)
        {
            //_currentSpeed = Mathf.Clamp(_currentSpeed + (accelerationCoefficient * _accelerationFactor), _minMoveSpeed, _maxMoveSpeed);
        }
        public void Turn(float angle)
        {
            //if (_currentSpeed <= 0f) return;

            Quaternion currentRotation = _engine.transform.rotation;
            Quaternion desiredRotation = currentRotation * Quaternion.Euler(angle * _rotationDirection * _rotationSpeed);
            _engine.transform.rotation = Quaternion.Slerp(currentRotation, desiredRotation, Time.deltaTime);
        }
        private void FixedUpdate()
        {
            MoveTrain();
        }
        void MoveTrain()
        {
           //_engine.transform.position = _engine.transform.position + _engine.transform.up * _currentSpeed * Time.deltaTime;
        }
        public void AddWagon()
        {
            _train.Add(new Wagon());
            _wagons.Add(Instantiate(_wagonPrefabs[_wagonPrefabs.Count - 1]).transform);
        }
    }
}