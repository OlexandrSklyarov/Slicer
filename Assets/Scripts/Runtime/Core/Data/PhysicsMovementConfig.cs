using UnityEngine;

namespace SA.Runtime.Core.Data
{
    [CreateAssetMenu(fileName = "PhysicsMovementConfig", menuName = "SO/Configs/PhysicsMovementConfig")]
    public class PhysicsMovementConfig : ScriptableObject
    {
        [field: SerializeField] public float MinForwardForce {get; private set;} = 5f;
        [field: SerializeField] public float MaxForwardForce {get; private set;} = 60f;
        [field: SerializeField] public float AddForwardForceTimeMultiplier {get; private set;} = 6f;
        [field: SerializeField] public float SubForwardForceTimeMultiplier {get; private set;} = 3f;
        [field: SerializeField] public AnimationCurve AddForwardForceCurve {get; private set;}
        [field: SerializeField] public AnimationCurve SubForwardForceCurve {get; private set;}
        [field: SerializeField, Min(1f)] public float DownForce {get; private set;} = 5f;
        [field: SerializeField, Min(1f)] public float UpForce {get; private set;} = 25f;
        [field: SerializeField, Range(0.1f, 1f)] public float ForceThreshold {get; private set;} = 0.9f;
    }
}