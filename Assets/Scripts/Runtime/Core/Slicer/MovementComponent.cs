using SA.Runtime.Core.Data;
using UnityEngine;

namespace SA.Runtime.Core.Slicer
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private SlicerMovementConfig _config;
        private float _rotationValue;

        public void Move()
        {
            transform.position = transform.position + transform.forward * _config.ForwardForce * Time.deltaTime;
            RotationProcess();
        }

        public void AddRotation()
        {
            if (!_config.IsCanRotate)
            {
                _rotationValue = 0f;
                return;
            }
            _rotationValue -= _config.RotateUpForce * Time.deltaTime;
        }

        private void RotationProcess()
        {
            if (!_config.IsCanRotate)
            {
                _rotationValue = 0f;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_rotationValue, 0f, 0f), _config.ChangeRotateTimeMultiplier * Time.deltaTime);
            _rotationValue += _config.RotateDownForce * Time.deltaTime;
            _rotationValue = Mathf.Clamp(_rotationValue, _config.RotationLimit.x, _config.RotationLimit.y);
        }
    }
}
