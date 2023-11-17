using System.Collections.Generic;
using SA.Runtime.Core.Data;
using UnityEngine;

namespace SA.Runtime.Core.Map
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private MapConfig _config;

        private Vector3 _lastPivot;
        private Vector3 _lastSlicerEnterPosition;
        private Queue<MapChank> _chankQueue = new();
        private int _spawnChankCounter;

        private void Start() 
        {
            for (int i = 0; i < _config.StartSpawnCount; i++)
            {
                SpawnChank();
            }
        }

        private void SpawnChank()
        {            
            var chank = Instantiate(GetRandomChank(), _lastPivot, Quaternion.identity, transform);
            chank.EnterSlicerEvent += OnSlicerEnter;
            _chankQueue.Enqueue(chank);

            _lastPivot = chank.Pivot.position;

            _spawnChankCounter++;
        }

        private MapChank GetRandomChank()
        {
            return _config.Chanks[UnityEngine.Random.Range(0, _config.Chanks.Length)];
        }

        private void DespawnLastChank()
        {
            var chank = _chankQueue.Dequeue();
            chank.Reclaim();
        }


        private void OnSlicerEnter(MapChank chank)
        {
            chank.EnterSlicerEvent -= OnSlicerEnter;
            _lastSlicerEnterPosition = chank.transform.position;

            TryCreateNextChank();
        }

        private void TryCreateNextChank()
        {
            if (_spawnChankCounter < _config.MaxChankPerLevel && IsNeedCreateNextChank())
            {
                DespawnLastChank();
                SpawnChank();
            }
        }        

        private bool IsNeedCreateNextChank()
        {
            var distance = transform.position - _lastSlicerEnterPosition;
            distance.y = 0f;
            return distance.magnitude >= _config.CreateNextChankDistance;
        }
    }
}
