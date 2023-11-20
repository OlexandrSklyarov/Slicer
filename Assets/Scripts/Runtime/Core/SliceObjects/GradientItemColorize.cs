using UnityEngine;

namespace SA.Runtime.Core.SliceObjects
{
    public class GradientItemColorize : MonoBehaviour
    {
        [SerializeField] private Gradient _colorGradient;
        [SerializeField] private SliceObjectView[] _items;

        private void Awake() 
        {
            for (int i = 0; i < _items.Length; i++)
            {
                var progress = (float)i / _items.Length;
                _items[i].SetColor(_colorGradient.Evaluate(progress));
            }
        }
    }
}