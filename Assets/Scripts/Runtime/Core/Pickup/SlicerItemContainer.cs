using System.Collections.Generic;
using UnityEngine;

namespace SA.Runtime.Core.Pickup
{
    public class SlicerItemContainer : MonoBehaviour
    {
        [SerializeField] private SlicerItem _prefab;
        [SerializeField] private int _spawnCount;

        private List<SlicerItem> _items;

        void Start()
        {
            _items = new List<SlicerItem>(_spawnCount);

            for (int i = 0; i < _spawnCount; i++)
            {
                var item = GetItem(transform.position + transform.forward * i);
                item.Init();
                item.PickupEvent += OnItemPickup;
                _items.Add(item);
            }
        }

        private void OnItemPickup(SlicerItem item)
        {
            item.PickupEvent -= OnItemPickup;
            _items.Remove(item);
        }

        private SlicerItem GetItem(Vector3 pos)
        {
            //TODO: implement from pool!!!
            return Instantiate(_prefab, pos, Quaternion.Euler(0f, 0f, -90f), transform);
        }

        private void Update()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].transform.Rotate(30f * Time.deltaTime * Vector3.right);
            }
        }
    }
}
