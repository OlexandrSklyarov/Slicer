using System.Collections.Generic;
using EzySlice;
using SA.Runtime.Core.Map;
using UnityEngine;

namespace SA.Runtime.Core.Slicer
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class SliceComponent : MonoBehaviour
    {
        private Transform _sliceItemsContainer;
        private void Awake() 
        {
            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;

            _sliceItemsContainer = new GameObject($"[SliceItemContainer]").transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out SliceTarget target))
            {
                Slice(target);
            }
        }

        private void Slice(SliceTarget target)
        {
            Debug.Log("<color=yellow>try slice</color>");

            var items = ShatterObjects(target.gameObject, target.CrossSectionMaterial);

            if (items != null && items.Length > 0) 
            {
                target.gameObject.SetActive(false);

                for (int i = 0; i < items.Length; i++)
                {
                    var item = items[i];
                    item.transform.SetParent(_sliceItemsContainer);
                    item.transform.position = target.transform.position;                    
                    item.AddComponent<MeshCollider>().convex = true;

                    var rb = item.AddComponent<Rigidbody>();
                    var modifier = (i % 2 == 0)? 0f : 5f;
                    rb.AddExplosionForce(100f, item.transform.position, 2f, modifier);

                    Destroy(item, 8f);

                    Debug.Log("<color=green>slice Success!!!</color>");
                }   
            }   
            else
            {
                Debug.LogError("slice failure!!!");
            }      
        }

        private GameObject[] ShatterObjects(GameObject source, Material crossSectionMaterial = null)
        {
            return source.SliceInstantiate
            (
                transform.position, 
                transform.up,               
                new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
                crossSectionMaterial
            );
        }

    }
}
