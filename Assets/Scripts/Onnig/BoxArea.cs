using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onnig
{
    public class BoxArea : MonoBehaviour
    {
        [SerializeField] private float _contractionSpeed = 1.0f;

        private void Start()
        {

        }

        private void Update()
        {
            if (transform.localScale.x > 0f)
            {
                float scaleAmount = _contractionSpeed * Time.deltaTime;
                transform.localScale -= new Vector3(scaleAmount, 0f, scaleAmount);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}