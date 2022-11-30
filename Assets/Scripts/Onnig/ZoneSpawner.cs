using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onnig
{
    public class ZoneSpawner : MonoBehaviour
    {
        public static ZoneSpawner Instance;

        [SerializeField] private ClicheZone _boxPrefab;
        [SerializeField] private OriginalityZone _cloudPrefab;
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
            Debug.Assert(Instance == null, "Only one instance of ZoneSpawner should ever be present!");
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
                ClicheZone newBox = Instantiate(_boxPrefab);
                newBox.transform.position = GetRandomSpawnPosition(0.01f);
            }
            _boxSpawnTimer += deltaTime;

            // cloud spawning
            if (_cloudSpawnTimer > _cloudSpawnInterval)
            {
                _cloudSpawnTimer -= _cloudSpawnInterval;
                OriginalityZone newCloud = Instantiate(_cloudPrefab);
                newCloud.transform.position = GetRandomSpawnPosition();
                newCloud.transform.rotation = GetRandomRotation();
            }
            _cloudSpawnTimer += deltaTime;
        }

        private Vector3 GetRandomSpawnPosition(float offsetY = 0)
        {
            Vector3 randomOffsetXZ = Vector3.Scale(_spawnSizeBounds, new Vector3(Random.value, 0f, Random.value));
            return _spawnMinBounds + (Vector3.up * offsetY) + randomOffsetXZ;
        }

        private Quaternion GetRandomRotation()
        {
            return Quaternion.AngleAxis(Random.value * 360.0f, Vector3.up);
        }
    }
}
