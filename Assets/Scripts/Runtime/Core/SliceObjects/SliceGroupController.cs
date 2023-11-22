using UnityEngine;

namespace SA.Runtime.Core.SliceObjects
{
    public class SliceGroupController : MonoBehaviour
    {
        [SerializeField] private SliceTarget[] _targets;

        private void Awake() 
        {
            foreach (var item in _targets)
            {
                item.SliceEvent += OnTargetSlice;
            }    
        }

        private void OnTargetSlice(SliceTarget target)
        {
            for (int i = 0; i < _targets.Length; i++)
            {
                var item = _targets[i];
            
                item.SliceEvent -= OnTargetSlice;

                if (item == target) continue;
                                    
                item.SetHideWithMinSize();                
            }
        }
    }
}
