using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kevin
{
    public class MarkerManager : MonoBehaviour
    {
        public class Marker
        {
            public Vector3 _position;
            public Quaternion _rotation;

            public Marker(Vector3 position, Quaternion rotation)
            {
                _position = position;
                _rotation = rotation;
            }
        }

        public List<Marker> _markersList = new List<Marker>();
        private void FixedUpdate()
        {
            UpdateMarkerList();
        }
        public void UpdateMarkerList()
        {
            _markersList.Add(new Marker(transform.position, transform.rotation));
        }
        public void ClearMarkerList()
        {
            _markersList.Clear();
            _markersList.Add(new Marker(transform.position, transform.rotation));
        }
    }
}