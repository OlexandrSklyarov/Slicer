using UnityEngine;

namespace SA.Runtime.Core.Data
{
    [CreateAssetMenu(fileName = "PhysicsMovementConfig", menuName = "SO/Configs/PhysicsMovementConfig")]
    public class PhysicsMovementConfig : ScriptableObject
    {
        [field: SerializeField, Min(0.1f)] public float MinForwardForce {get; private set;} = 5f;
        [field: SerializeField, Min(0.1f)] public float MaxForwardForce {get; private set;} = 60f;        
        [field: SerializeField] public AnimationCurve ForwardForceCurve {get; private set;}       
        [field: SerializeField, Min(1f)] public float DownForce {get; private set;} = 5f;
        [field: SerializeField, Min(1f)] public float UpForce {get; private set;} = 25f;
    }
}