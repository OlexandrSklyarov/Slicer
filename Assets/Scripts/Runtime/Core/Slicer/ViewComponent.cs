using UnityEngine;

namespace SA.Runtime.Core.Slicer
{
    [RequireComponent(typeof(Rigidbody))]
    public class ViewComponent : MonoBehaviour
    {
        [SerializeField] private Transform _slicerModel;

        private Rigidbody _rb;

        private void Awake() 
        {
            _rb = GetComponent<Rigidbody>();  
        }

        private void LateUpdate() 
        {
            var dir = _rb.velocity.normalized;
            var rot = Mathf.Atan2(dir.y, dir.z) * Mathf.Rad2Deg;
            _slicerModel.rotation = Quaternion.Euler(-rot, 0f, 0f);
        }
    }
}
