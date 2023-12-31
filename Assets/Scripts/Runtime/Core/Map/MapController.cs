using System;
using System.Collections.Generic;
using DG.Tweening;
using SA.Runtime.Core.Data;
using UnityEngine;

namespace SA.Runtime.Core.Map
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private MapConfig _config;
        [SerializeField] private MapChank _startChank;

        private Vector3 _nextChankSpawnPoint;
        private Queue<MapChank> _chankQueue = new();
        private int _spawnChankCounter;
        private int _enterChankCounter;
        private bool _finishSpawned;

        public event Action FinishEvent;

        public void Init() 
        {
            InitChank(_startChank);

            for (int i = 0; i < _config.StartSpawnCount; i++)
            {
                SpawnChank(true);
            }
        }

        private void SpawnChank(bool isSpawnAnimation = false)
        {
            var chank = Instantiate(GetRandomChank(), _nextChankSpawnPoint, Quaternion.identity, transform);
            
            InitChank(chank);

            if (isSpawnAnimation)
            {
                var targetPos = chank.transform.position;
                chank.transform.position = targetPos + Vector3.up * 100f;
                chank.transform.DOMoveY(targetPos.y, 1f);
            }
        }

        private void InitChank(MapChank chank)
        {
            chank.EnterSlicerEvent += OnSlicerEnter;

            chank.name = $"{chank.name}[{chank.Pivot.position.z}]";

            _nextChankSpawnPoint = chank.Pivot.position + transform.forward * _config.DistanceToNextChank;

            _chankQueue.Enqueue(chank);
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

            _enterChankCounter++;

            TryCreateNextChank();
        }

        private void TryCreateNextChank()
        {
            if (_spawnChankCounter >= _config.MaxChankPerLevel)
            {
                SpawnFinish();
                return;
            }

            if (IsNeedCreateNextChank())
            {
                DespawnLastChank();
                SpawnChank(true);
            }
        }

        private bool IsNeedCreateNextChank()
        {
            return _enterChankCounter >= 3;
        }

        private void SpawnFinish()
        {
            if (_finishSpawned) return;

            _finishSpawned = true;
            var finish = Instantiate(_config.Finish, _nextChankSpawnPoint, Quaternion.identity, transform);
            finish.EnterSlicerEvent += OnSlicerFinished;
        }

        private void OnSlicerFinished(MapChank finish)
        {
            finish.EnterSlicerEvent -= OnSlicerFinished;
            FinishEvent?.Invoke();
            Debug.Log($"<color=green> FINISH!!! </color>");
        }
    }
}
