using System;
using UnityEngine;

namespace SA.Runtime.Core.Pickup
{    
    [RequireComponent(typeof(BoxCollider))]
    public class SlicerItem : MonoBehaviour, IPickupItem
    {
        Transform IPickupItem.MyTransform => transform;

        private Collider _collider;

        public event Action<SlicerItem> PickupEvent;

        
        public void Init() 
        {
            _collider ??= GetComponent<Collider>(); 
            _collider.enabled = true;
        }

        public void Reclaim() 
        {            
            Destroy(this.gameObject);
        } 

        void IPickupItem.Pickup() 
        {
            _collider.enabled = false;
            PickupEvent?.Invoke(this);
        }
    }
}