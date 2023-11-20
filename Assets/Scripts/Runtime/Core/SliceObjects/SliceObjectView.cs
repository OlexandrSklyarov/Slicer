using UnityEngine;

namespace SA.Runtime.Core.SliceObjects
{
    public class SliceObjectView : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private int _changeMaterialIndex;

        public void SetColor(Color color)
        {
            _renderer.materials[_changeMaterialIndex].color = color;
        }
    }
}