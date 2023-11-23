using SA.Runtime.Core.Slicer;
using UnityEngine;

namespace SA.Runtime.Core.Data
{
    [CreateAssetMenu(fileName = "SliceTargetConfig", menuName = "SO/Configs/SliceTargetConfig")]
    public class SliceTargetConfig : ScriptableObject
    {
        [field: SerializeField] public Material CrossSectionMaterial { get; private set; }
        [field: SerializeField] public PhysicalParts PhysicalType{ get; private set; }
        [field: SerializeField] public Vector3 SliceForceAxis { get; private set; } = Vector3.up;
        [field: SerializeField, Min(1)] public int Points { get; private set; } = 5;
    }
}