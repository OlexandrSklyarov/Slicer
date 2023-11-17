using UnityEngine;

namespace SA.Runtime.Core.Slicer
{
    [RequireComponent(typeof(MovementComponent), typeof(SliceComponent), typeof(InputController))]
    public class SlicerController : MonoBehaviour
    {
        private MovementComponent _movement;
        private InputController _input;
        
        private void Awake() 
        {
            _movement = GetComponent<MovementComponent>();
            _input = GetComponent<InputController>();
        }

        private void Update() 
        {
            _movement.Move();

            if (_input.IsPressed)
            {
                _movement.AddRotation();
            }
        }
    }
}