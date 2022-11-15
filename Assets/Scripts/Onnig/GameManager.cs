using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onnig
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private BoxArea _boxPrefab;
        [SerializeField] private CloudArea _cloudPrefab;
        [SerializeField] private Collider _spawnableArea;
        [SerializeField] private float _boxSpawnInterval = 1.0f;
        [SerializeField] private float _cloudSpawnInterval = 2.0f;

        private Vector3 _spawnMinBounds;
        private Vector3 _spawnSizeBounds; // TODO : check if worth
        private float _boxSpawnTimer;
        private float _cloudSpawnTimer;

        private void Awake()
        {
            // init singleton
            Debug.Assert(Instance == null, "Only one instance of GameManager should ever be present!");
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            _spawnMinBounds = Vector3.Scale(_spawnableArea.bounds.min, new Vector3(1.0f, 0f, 1.0f));
            _spawnSizeBounds = _spawnableArea.bounds.size;
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            // box spawning
            if (_boxSpawnTimer > _boxSpawnInterval)
            {
                _boxSpawnTimer -= _boxSpawnInterval;
                BoxArea newBox = Instantiate(_boxPrefab);
                newBox.transform.position = GetRandomSpawnPosition();
                //newBox.transform.rotation = GetRandomRotation();
            }
            _boxSpawnTimer += deltaTime;

            // cloud spawning
            if (_cloudSpawnTimer > _cloudSpawnInterval)
            {
                _cloudSpawnTimer -= _cloudSpawnInterval;
                CloudArea newCloud = Instantiate(_cloudPrefab);
                newCloud.transform.position = GetRandomSpawnPosition();
                newCloud.transform.rotation = GetRandomRotation();
            }
            _cloudSpawnTimer += deltaTime;
        }

        private Vector3 GetRandomSpawnPosition()
        {
            return _spawnMinBounds + Vector3.Scale(_spawnSizeBounds, new Vector3(Random.value, 0f, Random.value));
        }

        private Quaternion GetRandomRotation()
        {
            return Quaternion.AngleAxis(Random.value * 360.0f, Vector3.up);
        }
    }
}
