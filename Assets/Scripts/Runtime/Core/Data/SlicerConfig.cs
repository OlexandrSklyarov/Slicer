using UnityEngine;

namespace SA.Runtime.Core.Data
{
    [CreateAssetMenu(fileName = "SlicerConfig", menuName = "SO/Configs/SlicerConfig")]
    public class SlicerConfig : ScriptableObject
    {
        [field: SerializeField, Min(1f)] public float SliceItemForce {get; private set;} = 5f;
    }
}