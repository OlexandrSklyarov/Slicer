using UnityEngine;

namespace SA.Runtime.Core.Slicer
{
    [RequireComponent(typeof(InputController))]
    public class SlicerController : MonoBehaviour
    {
        [SerializeField] private PhysicsMovement _movement;

        private InputController _input;
        
        private void Awake() 
        {
            _input = GetComponent<InputController>();
        }

        private void Update() 
        {
            if (_input.IsPressed)
            {
                _movement.AddUpForce();
            }
        }
    }
}