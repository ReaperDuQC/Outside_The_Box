using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kevin
{
    public class TrainController : MonoBehaviour
    {
        [SerializeField] float _currentspeed;
        [SerializeField] float _maxMoveSpeed = 100f;
        [SerializeField] float _rotationSpeed = 5f;

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
        public void AddVelocity(float velocity)
        {

        }
        public void Turn(float angle)
        {

        }
    }
}