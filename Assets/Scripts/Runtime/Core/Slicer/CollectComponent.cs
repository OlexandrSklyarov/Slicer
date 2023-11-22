using System.Collections.Generic;
using System.Linq;
using SA.Runtime.Core.Pickup;
using UnityEngine;

namespace SA.Runtime.Core.Slicer
{
    public class CollectComponent : MonoBehaviour
    {
        private List<IPickupItem> _items = new();

        public void RemoveItem()
        {
            if (_items.Count <= 0) return;

            var item = _items.Last();
            _items.Remove(item);

            item.Reclaim();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPickupItem item))
            {
                item.Pickup();
                item.MyTransform.SetParent(null);
                _items.Add(item);                
            }
        }       

        private void SetItemPositionAndRotation(IPickupItem item, Transform origin)
        {            
            var dir = (origin.position - item.MyTransform.position).normalized;
            var rotAngle = Mathf.Atan2(dir.y, dir.z) * Mathf.Rad2Deg;

            item.MyTransform.rotation = Quaternion.Euler(-rotAngle, 0f, 0f);

            item.MyTransform.position = origin.position + 
                (item.MyTransform.position - origin.position).normalized * 0.2f;           
        }
       

        private void LateUpdate()
        {
            if (_items.Count > 0)
                SetItemPositionAndRotation(_items[0], transform);

            for (int i = 1; i < _items.Count; i++)
            {
                var origin = _items[i-1].MyTransform;
                SetItemPositionAndRotation(_items[i], origin);
            }
        }
    }
}