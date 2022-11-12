using static UnityEngine.GridBrushBase;
using System.Collections.Generic;
using UnityEngine;

namespace Kevin
{
    public class TrainController2 : MonoBehaviour
    {
        [SerializeField] float _currentSpeed;
        [SerializeField] float _accelerationFactor = 0.5f;
        [SerializeField] float _minMoveSpeed = -5f;
        [SerializeField] float _maxMoveSpeed = 100f;
        [SerializeField] Vector3 _rotationDirection;
        [SerializeField] float _rotationSpeed = 5f;
        [SerializeField] Transform _enginAnchorPoint;

        [SerializeField] GameObject _engine;
        [SerializeField] GameObject _wagonPrefabs;
        [SerializeField] HingeJoint _engineJoint;
        [SerializeField] List<GameObject> _wagons;
        [SerializeField] int _nbWagons;
        int _currentWagon = -1;
        int _nextWagon = 0;

        [SerializeField] bool _debug;
        [SerializeField] float _debugRayDistance = 5f;

        private void Awake()
        {
            AddRigidBody(_engine);
            AddHingeJoint(_engine);
             _engineJoint = _engine.GetComponent<HingeJoint>();
            _engineJoint.anchor = new Vector3( 0f,-0.5f, 0f);
        }
        private void Start()
        {
            for (int i = 0; i < _nbWagons; i++)
            {
                AddWagon();
            }
        }
        private void OnDrawGizmos()
        {
            if (!_debug) return;
        }
        public void Accelerate(float accelerationCoefficient)
        {
            _currentSpeed = Mathf.Clamp(_currentSpeed + (accelerationCoefficient * _accelerationFactor), _minMoveSpeed, _maxMoveSpeed);
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
            _engine.transform.position = Vector3.Lerp(_engine.transform.position, _engine.transform.position + _engine.transform.up * _currentSpeed, Time.deltaTime);
            _engine.transform.position = Vector3.Lerp(_engine.transform.position, _engine.transform.position + _engine.transform.up * _currentSpeed, Time.deltaTime);
        }
        void AddHingeJoint(GameObject gameObject)
        {
            if (!gameObject.GetComponent<HingeJoint>())
            {
                gameObject.AddComponent<HingeJoint>();
            }
        }
        void AddRigidBody(GameObject gameObject)
        {
            if (!gameObject.GetComponent<Rigidbody>())
            {
                Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>();
                rigidBody.useGravity = false;
            }
        }
        public void AddWagon()
        {
            if (_wagonPrefabs == null) return;

            GameObject wagon = Instantiate(_wagonPrefabs);
            _wagons.Add(wagon);
            Transform transformReference = _currentWagon == -1 ? _engine.transform : _wagons[_currentWagon].transform;
            wagon.transform.position = transformReference.position + -transformReference.up * (Vector3.Magnitude(transformReference.position - transformReference.GetChild(0).position) * 2);
            AddRigidBody(wagon);
            ConnectWagon(_currentWagon, _nextWagon);

            _currentWagon = _nextWagon;
            _nextWagon++;
        }
        void ConnectWagon(int anchorWagon, int wagonToConnect)
        {
            if (anchorWagon == -1)
            {
                _engineJoint.connectedBody = _wagons[wagonToConnect].GetComponent<Rigidbody>();
            }
            else
            {
                HingeJoint hinge = _wagons[anchorWagon].AddComponent<HingeJoint>();
                hinge.anchor = _engineJoint.anchor;
                hinge.connectedBody = _wagons[wagonToConnect].GetComponent<Rigidbody>();
            }
        }
    }
}