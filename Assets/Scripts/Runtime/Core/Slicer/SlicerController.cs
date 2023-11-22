using System;
using UnityEngine;

namespace SA.Runtime.Core.Slicer
{
    public class SlicerController : MonoBehaviour
    {
        public int CollectedPoints => _points;
        [field: SerializeField] public Transform LookCameraPoint {get; private set;}

        [SerializeField] private PhysicsMovement _movement;
        [SerializeField] private SliceComponent _slicer;
        [SerializeField] private InputController _input;

        private int _points;
        private bool _isCanMove;

        public event Action DamageEvent;
        
        public void Init() 
        {            
            _movement.Init();
            
            _slicer.Init();
            _slicer.DamageEvent += OnDamage;
            _slicer.SliceEvent += OnAddPoints;
        }

        private void OnAddPoints(int points) => _points += points;

        public void ActiveControl(bool isCanMove) => _isCanMove = isCanMove;

        private void OnDamage() 
        {
            _slicer.DamageEvent -= OnDamage;
            _isCanMove = false;
            DamageEvent?.Invoke();
        }

        private void Update() 
        {
            if (!_isCanMove) return;

            if (_input.IsPressed)
            {
                _movement.AddUpForce();
            }
        }
    }
}