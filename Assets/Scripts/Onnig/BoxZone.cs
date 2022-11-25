using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onnig
{
    public class BoxZone : MonoBehaviour
    {
        [SerializeField] private float _contractionSpeed = 1.0f;

        private void Start()
        {

        }

        private void FixedUpdate()
        {
            if (transform.localScale.x > 0f)
            {
                float scaleAmount = _contractionSpeed * Time.deltaTime;
                transform.localScale -= new Vector3(scaleAmount, 0f, scaleAmount);

                // prevent negative size collision bugs
                if (transform.localScale.x < 0f)
                {
                    transform.localScale = Vector3.zero;
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}