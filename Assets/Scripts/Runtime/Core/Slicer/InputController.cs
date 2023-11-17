using UnityEngine;
using UnityEngine.InputSystem;

namespace SA.Runtime.Core.Slicer
{
    public class InputController : MonoBehaviour
    {
        public bool IsPressed => Mouse.current.leftButton.isPressed;
    }
}