using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Kevin
{
    public class Wagon : MonoBehaviour
    {
        [SerializeField] List<Transform> _anchorPoints;

        private void Awake()
        {
           foreach(Transform t in transform)
           {
                _anchorPoints.Add(t);
           }
        }
    }
}