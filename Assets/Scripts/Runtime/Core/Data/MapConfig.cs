using SA.Runtime.Core.Map;
using UnityEngine;

namespace SA.Runtime.Core.Data
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "SO/Configs/MapConfig")]
    public class MapConfig : ScriptableObject
    {
        [field: SerializeField] public MapChank[] Chanks {get; private set;}
        [field: SerializeField, Min(1f)] public float CreateNextChankDistance {get; private set;} = 300f;
        [field: SerializeField, Min(1)] public int MaxChankPerLevel {get; private set;} = 15;
        [field: SerializeField, Min(1)] public int StartSpawnCount {get; private set;} = 4;
    }
}