using EzySlice;
using SA.Runtime.Core.Data;
using SA.Runtime.Core.Map;
using UnityEngine;

namespace SA.Runtime.Core.Slicer
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class SliceComponent : MonoBehaviour
    {
        [SerializeField] private SlicerConfig _config;
        private Transform _sliceItemsContainer;

        private const string SLICE_PEACE_LAYER_NAME = "SlicePeace";

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
            var items = ShatterObjects(target.gameObject, target.CrossSectionMaterial);

            if (items != null && items.Length > 0) 
            {
                target.gameObject.SetActive(false);

                for (int i = 0; i < items.Length; i++)
                {
                    var isUpperItem = i % 2 == 0;

                    var item = items[i];

                    item.transform.SetParent(_sliceItemsContainer);
                    item.transform.position = target.transform.position;     

                    item.AddComponent<MeshCollider>().convex = true;
                    item.layer = LayerMask.NameToLayer(SLICE_PEACE_LAYER_NAME);
                   
                    if (target.PhysicalType == PhysicalParts.Both ||
                        target.PhysicalType == PhysicalParts.Upper && isUpperItem ||
                        target.PhysicalType == PhysicalParts.Lower && !isUpperItem)
                    {
                        ItemAddForce(item, isUpperItem);
                    }
                                           
                    Destroy(item, 8f);
                }   
            }   
            else
            {
                Debug.LogError("slice failure!!!");
            }      
        }

        private void ItemAddForce(GameObject item, bool isUpperItem)
        {
            var modifier = (isUpperItem)? 1f : -1f;
            var rb = item.AddComponent<Rigidbody>();
            var force = _config.SliceItemForce * modifier * Vector3.up;
            rb.AddForce(force, ForceMode.Impulse);
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
