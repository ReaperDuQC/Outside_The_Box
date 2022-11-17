using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onnig
{
    public class CloudArea : MonoBehaviour
    {
        [SerializeField] private float _expansionSpeed = 0.5f;
        [SerializeField] private float _contractionSpeed = 1.0f;

        private bool _isExpanding = true;

        private void Start()
        {
            
        }

        private void Update()
        {
            float scaleAmount = Time.deltaTime;

            if (_isExpanding)
            {
                scaleAmount *= _expansionSpeed;
                transform.localScale += new Vector3(scaleAmount, 0f, scaleAmount);
            }
            else if (transform.localScale.x > 0f)
            {
                scaleAmount *= _contractionSpeed;
                transform.localScale -= new Vector3(scaleAmount, 0f, scaleAmount);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trigger enter");

            // stop expanding on collision with train locomotive
            if (other.GetComponent<LocomotiveMovement>() != null)
            {
                Debug.Log("trigger enter train");

                _isExpanding = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("collision enter");

            // stop expanding on collision with train locomotive
            if (collision.gameObject.GetComponent<LocomotiveMovement>() != null)
            {
                Debug.Log("collision enter train");

                _isExpanding = false;
            }
        }
    }
}
