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

        private void Awake() 
        {
            _rb = GetComponent<Rigidbody>();    
            _force = transform.forward;
            _forwardForce = _config.MinForwardForce;
        }

        private void Update()
        {
            _force = transform.forward * _forwardForce;
            _force += _config.DownForce * -1f * transform.up;

            CalculateForwardForce();
        }

        private void CalculateForwardForce()
        {
            var dot = Vector3.Dot(Vector3.forward, _rb.velocity.normalized);
            var progress = Mathf.Clamp01(dot);
            _forwardForce = Mathf.Lerp(_config.MinForwardForce, _config.MaxForwardForce, progress);
        }

        public void AddUpForce()
        {            
            _force += transform.up * _config.UpForce;
        }

        private void FixedUpdate()
        {
            _rb.AddForce(_force);

            var vel = _rb.velocity;
            vel.z = Mathf.Clamp(vel.z, 0f, _config.MaxForwardForce);
            _rb.velocity = vel;
        }
    }
}
