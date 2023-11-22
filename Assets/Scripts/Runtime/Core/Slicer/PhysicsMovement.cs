using SA.Runtime.Core.Data;
using UnityEngine;

namespace SA.Runtime.Core.Slicer
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsMovement : MonoBehaviour
    {
        [SerializeField] private PhysicsMovementConfig _config;

        private Rigidbody _rb;
        private Vector3 _force;
        private float _forwardForce;
        private bool _isInit;

        public void Init()
        {
            _rb = GetComponent<Rigidbody>(); 
            _force = transform.forward;
            _forwardForce = _config.MinForwardForce;

            _isInit = true;
        }

        private void Update()
        {
            if (!_isInit) return;

            _force = transform.forward * _forwardForce;
            _force += _config.DownForce * -1f * transform.up;

            CalculateForwardForce();           
        }

        private void FixedUpdate()
        {
            if (!_isInit) return;
            
            _rb.AddForce(_force);

            var vel = _rb.velocity;
            vel.z = Mathf.Clamp(vel.z, 0f, _config.MaxForwardForce);
            _rb.velocity = vel;
        }        

        private void CalculateForwardForce()
        {
            var dot = Vector3.Dot(Vector3.forward, _rb.velocity.normalized);
            var clamp = Mathf.Clamp01(dot);
            var progress = _config.ForwardForceCurve.Evaluate(clamp);
            _forwardForce = Mathf.Lerp(_config.MinForwardForce, _config.MaxForwardForce, progress);
        }

        public void AddUpForce()
        {            
            _force += transform.up * _config.UpForce;
        }
    }
}
