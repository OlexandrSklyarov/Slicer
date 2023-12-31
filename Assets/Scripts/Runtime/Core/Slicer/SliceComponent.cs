using System;
using EzySlice;
using SA.Runtime.Core.Data;
using SA.Runtime.Core.Map;
using SA.Runtime.Core.SliceObjects;
using UnityEngine;

namespace SA.Runtime.Core.Slicer
{
    [RequireComponent(typeof(BoxCollider))]
    public class SliceComponent : MonoBehaviour
    {
        [SerializeField] private SlicerConfig _config;
        [SerializeField] private ParticleSystem _SliceVFX;

        private Transform _sliceItemsContainer;
        
        private bool _isBlockInteract;

        private const string SLICE_PEACE_LAYER_NAME = "SlicePeace";

        public event Action DamageEvent;
        public event Action<int> SliceEvent;

        public void Init() 
        {
            _sliceItemsContainer = new GameObject($"[SliceItemContainer]").transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isBlockInteract) return;
            
            if (other.GetComponent<Damager>() != null)
            {
                _isBlockInteract = true;
                DamageEvent?.Invoke();

                return;
            }

            if (other.TryGetComponent(out SliceTarget target))
            {
                SliceEvent?.Invoke(target.Config.Points);

                target.OnSlice();
                Slice(target);
                _SliceVFX.Play();
            }
        }

        private void Slice(SliceTarget target)
        {
            var items = ShatterObjects(target.gameObject, target.Config.CrossSectionMaterial);

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
                   
                    if (target.Config.PhysicalType == PhysicalParts.Both ||
                        target.Config.PhysicalType == PhysicalParts.Upper && isUpperItem ||
                        target.Config.PhysicalType == PhysicalParts.Lower && !isUpperItem)
                    {
                        ItemAddForce(item, isUpperItem, target.Config.SliceForceAxis);
                    }
                                           
                    Destroy(item, 8f);
                }   
            }   
            else
            {
                Debug.LogWarning($"slice item {target.name} failure!!!");
            }      
        }

        private void ItemAddForce(GameObject item, bool isUpperItem, Vector3 sliceForceAxis)
        {
            var modifier = (isUpperItem)? 1f : -1f;
            var rb = item.AddComponent<Rigidbody>();
            var force = _config.SliceItemForce * (modifier * sliceForceAxis).normalized;
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
