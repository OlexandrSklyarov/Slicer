using System;
using UnityEngine;

namespace SA.Runtime.Core.Slicer
{
    [RequireComponent(typeof(InputController))]
    public class SlicerController : MonoBehaviour
    {
        [field: SerializeField] public Transform LookCameraPoint {get; private set;}

        [SerializeField] private PhysicsMovement _movement;
        [SerializeField] private SliceComponent _slicer;

        private InputController _input;

        private bool _isCanMove;

        public event Action DamageEvent;
        public event Action CompleteEvent;
        
        private void Start() 
        {
            _input = GetComponent<InputController>();
            _slicer.DamageEvent += OnDamage;
        }

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