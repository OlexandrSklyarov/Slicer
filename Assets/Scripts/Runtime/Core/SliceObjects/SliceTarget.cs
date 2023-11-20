using SA.Runtime.Core.Slicer;
using UnityEngine;

namespace SA.Runtime.Core.SliceObjects
{
    public class SliceTarget : MonoBehaviour
    {
        [field: SerializeField] public Material CrossSectionMaterial { get; private set; }
        [field: SerializeField] public PhysicalParts PhysicalType{ get; private set; }
    }
}
