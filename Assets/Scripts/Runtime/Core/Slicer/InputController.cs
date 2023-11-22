using UnityEngine;
using UnityEngine.InputSystem;

namespace SA.Runtime.Core.Slicer
{
    public class InputController : MonoBehaviour
    {
        // private PlayerInputAction _inputAction;
        // private bool _isPressed;

        //public bool IsPressed => Touchscreen.current.primaryTouch.press.isPressed;
        public bool IsPressed => Touchscreen.current.primaryTouch.press.isPressed;
        // {
        //     _inputAction = new PlayerInputAction();  
        //     _inputAction.Enable();   

        //     _inputAction.Touch.PointerPress.started += (_) => {_isPressed = true;}; 
        //     _inputAction.Touch.PointerPress.canceled += (_) => {_isPressed = false;};  
        // }

        // private void OnDestroy() 
        // {
        //     _inputAction?.Disable(); 
        // }
    }
}