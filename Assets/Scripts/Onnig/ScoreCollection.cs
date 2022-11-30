using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onnig
{
    public class ScoreCollection : MonoBehaviour
    {
        static public float PointsCliche { get; private set; } // TODO : reset to 0 function on new game
        static public float PointsOriginality { get; private set; }

        private bool _isInClicheZone;
        private bool _isInOriginalityZone;

        private void Start()
        {

        }

        private void FixedUpdate()
        {
            // collect points
            if (_isInClicheZone)
            {
                PointsCliche += Time.fixedDeltaTime;
            }
            else if (_isInOriginalityZone)
            {
                PointsOriginality += Time.fixedDeltaTime;
            }

            // reset flags
            _isInClicheZone = false;
            _isInOriginalityZone = false;
        }

        private void OnTriggerStay(Collider other)
        {
            // check if on any collection zone 
            if (other.GetComponent<ClicheZone>() != null)
            {
                _isInClicheZone = true;
            }
            else if (other.GetComponent<OriginalityZone>() != null)
            {
                _isInOriginalityZone = true;
            }
        }

        static public void ResetScore()
        {
            PointsCliche = 0;
            PointsOriginality = 0;
        }
    }
}
