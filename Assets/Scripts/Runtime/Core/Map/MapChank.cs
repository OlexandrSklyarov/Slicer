using System;
using UnityEngine;
using SA.Runtime.Core.Slicer;

namespace SA.Runtime.Core.Map
{
    [RequireComponent(typeof(BoxCollider))]
    public class MapChank : MonoBehaviour
    {
        [field: SerializeField] public Transform Pivot {get; private set;}

        public event Action<MapChank> EnterSlicerEvent;

        public void Reclaim()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SlicerController>() != null)
            {
                EnterSlicerEvent?.Invoke(this);
                Debug.Log("Enter SlicerController");
            }
        }
    }
}
