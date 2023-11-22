using UnityEngine;

namespace SA.Runtime.Core.Slicer
{
    public class InputController : MonoBehaviour
    {
        private PlayerInputAction _inputAction;
        private bool _isPressed;

        public bool IsPressed => _isPressed;

        private void Awake() 
        {
            _inputAction = new PlayerInputAction();  
            _inputAction.Enable();   

            _inputAction.Touch.PointerPress.started += (ctx) => {_isPressed = true;  Debug.Log($"performed {_isPressed}"); }; 
            _inputAction.Touch.PointerPress.canceled += (_) => {Debug.Log("canceled"); _isPressed = false;  };  
        }

        private void OnDestroy() 
        {
            _inputAction?.Disable(); 
        }
    }
}