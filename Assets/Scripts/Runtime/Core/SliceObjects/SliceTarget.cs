using System;
using DG.Tweening;
using SA.Runtime.Core.Data;
using UnityEngine;

namespace SA.Runtime.Core.SliceObjects
{
    public class SliceTarget : MonoBehaviour
    {
        [field: SerializeField] public SliceTargetConfig Config { get; private set; }

        private Collider[] _colliders;

        public event Action<SliceTarget> SliceEvent;

        private void Awake() 
        {
            _colliders = GetComponents<Collider>();
        }

        public void OnSlice() => SliceEvent?.Invoke(this);

        public void SetSolid() => Array.ForEach(_colliders, x => x.isTrigger = false);

        public void SetHideWithMinSize() 
        {
            Array.ForEach(_colliders, x => x.enabled = false);

            transform
                .DOScale(Vector3.one * 0.01f, 1f)
                .OnComplete(()=> gameObject.SetActive(false));
        } 

        public void SetPhysicalBehavior(Vector3 force)
        {            
            SetSolid();

            if (!gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb = gameObject.AddComponent<Rigidbody>();  
            }

            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}
