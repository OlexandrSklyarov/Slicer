using SA.Runtime.Core.Data;
using UnityEngine;

namespace SA.Runtime.Core.Slicer
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsMovement : MonoBehaviour
    {
        [SerializeField] private PhysicsMovementConfig _config;
        private float _rotationValue;
        private Rigidbody _rb;
        private Vector3 _force;
        private float _forwardForce;
        private bool _isAccelerate;

        private void Awake() 
        {
            _rb = GetComponent<Rigidbody>();    
        }

        private void Update()
        {
            _force = transform.up * -1f * _config.DownForce;        

            if (_isAccelerate)
            {
                _forwardForce = Mathf.Lerp(_forwardForce, _config.MaxForwardForce, Time.deltaTime * _config.AddForwardForceTimeMultiplier);
                _isAccelerate = false;
            }   
            else
            {
                _forwardForce = Mathf.Lerp(_forwardForce, _config.MinForwardForce, Time.deltaTime * _config.SubForwardForceTimeMultiplier);
            }
        }

        public void AddForce()
        {
            _force = transform.forward * _forwardForce;
            _force += transform.up * _config.UpForce;
            _isAccelerate = true;
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
