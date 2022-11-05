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
            _targetPosition = _target.position;
        }
        private void LateUpdate()
        {
            _desiredPosition = _target.position;// Vector3.Lerp(_targetPosition, _target.position, /*_followSpeed **/ Time.deltaTime);
            _targetPosition = _target.position;
            transform.position = _desiredPosition + _offsetPosition;
        }
    }
}