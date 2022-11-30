using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onnig
{
    public class ClicheZone : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Collider _collider;
        [SerializeField] private float _contractionSpeed = 1.0f;
        [SerializeField] private float _spawnDelay = 0.5f;
        [SerializeField] private float _spawnAlpha = 0.5f;

        private float _delayTimer;
        private bool _isActive;

        private void Start()
        {
            SetAlpha(_spawnAlpha);
            _collider.enabled = false;
        }

        private void FixedUpdate()
        {
            // delay activation
            if (_delayTimer < _spawnDelay)
            {
                _delayTimer += Time.fixedDeltaTime;
                return;
            }
            else if (!_isActive)
            {
                _isActive = true;
                SetAlpha(1.0f);
                _collider.enabled = true;
            }

            // zone contraction
            if (transform.localScale.x > 0f)
            {
                float scaleAmount = _contractionSpeed * Time.fixedDeltaTime;
                transform.localScale -= new Vector3(scaleAmount, 0f, scaleAmount);

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

        private void SetAlpha(float alpha)
        {
            Color alphaColor = _meshRenderer.material.color;
            alphaColor.a = alpha;
            _meshRenderer.material.color = alphaColor;
        }
    }
}