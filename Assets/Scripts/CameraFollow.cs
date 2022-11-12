using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Kevin
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] Vector3 _offsetPosition;
        [SerializeField] Vector3 _desiredPosition;
        Vector3 _targetPosition;
        [SerializeField] float _followSpeed = 5f;
        void Start()
        {
            if(_target == null) return;

            _targetPosition = _target.position;
        }
        private void LateUpdate()
        {
            if (_target == null) return;

            _desiredPosition = _target.position;// Vector3.Lerp(_targetPosition, _target.position, /*_followSpeed **/ Time.deltaTime);
            _targetPosition = _target.position;
            transform.position = _desiredPosition + _offsetPosition;
        }
        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}