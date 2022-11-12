using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GridBrushBase;

namespace Kevin
{
    public class TrainController : MonoBehaviour
    {
        [SerializeField] float _distanceBetween = .2f;

        [SerializeField] float _currentSpeed;
        [SerializeField] float _accelerationFactor = 0.5f;
        [SerializeField] float _minMoveSpeed = 0f;
        [SerializeField] float _maxMoveSpeed = 100f;

        [SerializeField] Vector3 _rotationDirection;
        [SerializeField] float _rotationSpeed = 180f;
        [SerializeField] Transform _enginAnchorPoint;

        [SerializeField] GameObject _engine;
        [SerializeField] List<GameObject> _wagonPrefabs;
        [SerializeField] List<MarkerManager> _train;

        float _countUp = 0f;

        [SerializeField] int _initialNbWagons;
        private void Awake()
        {
            if (!_engine.GetComponent<MarkerManager>())
            {
                _engine.AddComponent<MarkerManager>();
            }
            _engine.transform.parent = transform;
            _train.Add(_engine.GetComponent<MarkerManager>());
        }
        public void Accelerate(float accelerationCoefficient)
        {
            _currentSpeed = Mathf.Clamp(_currentSpeed + (accelerationCoefficient * _accelerationFactor), _minMoveSpeed, _maxMoveSpeed);
        }
        public void Turn(float angle)
        {
            if (_currentSpeed <= 0f) return;

            Quaternion currentRotation = _engine.transform.rotation;
            Quaternion desiredRotation = currentRotation * Quaternion.Euler(angle * _rotationDirection * _rotationSpeed);
            _engine.transform.rotation = Quaternion.Slerp(currentRotation, desiredRotation, Time.deltaTime);
        }
        private void FixedUpdate()
        {
            if (_train.Count < _initialNbWagons)
            {
                AddWagon();
            }
            MoveTrain();
        }
        void MoveTrain()
        {
            _engine.transform.position = _engine.transform.position + _engine.transform.up * _currentSpeed * Time.deltaTime;

            for (int i = 1; i < _train.Count; i++)
            {
                MarkerManager marker = _train[i - 1];
                _train[i].transform.position = marker._markersList[0]._position;
                _train[i].transform.rotation = marker._markersList[0]._rotation;
                marker._markersList.RemoveAt(0);
            }
        }
        public void AddWagon()
        {
            if (_wagonPrefabs.Count <= 0) return;
            MarkerManager markManager = _train[_train.Count - 1].GetComponent<MarkerManager>();

            if (_countUp == 0)
            {
                markManager.ClearMarkerList();
            }
            _countUp += Time.deltaTime;

            if (_countUp >= _distanceBetween)
            {
                GameObject wagon = Instantiate(_wagonPrefabs[_wagonPrefabs.Count - 1], markManager._markersList[0]._position, markManager._markersList[0]._rotation, transform);
                if (!wagon.GetComponent<MarkerManager>())
                {
                    wagon.AddComponent<MarkerManager>();
                }
                MarkerManager mark = wagon.GetComponent<MarkerManager>();
                mark.ClearMarkerList();
                _countUp = 0f;
                _train.Add(mark);
            }
        }
    }
}