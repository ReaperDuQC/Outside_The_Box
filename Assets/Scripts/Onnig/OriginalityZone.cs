using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onnig
{
    public class OriginalityZone : MonoBehaviour // TODO : rename to originality?
    {
        [SerializeField] private float _expansionSpeed = 0.5f;
        [SerializeField] private float _contractionSpeed = 1.0f;

        private bool _isExpanding = true;

        private void Start()
        {

        }

        private void FixedUpdate()
        {
            // expansion logic
            if (_isExpanding)
            {
                float expansionAmount = _expansionSpeed * Time.fixedDeltaTime;
                transform.localScale += new Vector3(expansionAmount, 0f, expansionAmount);
            }
            // contraction logic
            else if (transform.localScale.x > 0f)
            {
                float contractionAmount = _contractionSpeed * Time.fixedDeltaTime;
                transform.localScale -= new Vector3(contractionAmount, 0f, contractionAmount);

                // prevent negative size collision bugs
                if (transform.localScale.x < 0f)
                {
                    transform.localScale = Vector3.zero;
                }
            }
            // destroy if scale is 0
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // stop expanding on collision with train locomotive
            if (other.GetComponent<LocomotiveMovement>() != null)
            {
                _isExpanding = false;
            }
        }
    }
}
