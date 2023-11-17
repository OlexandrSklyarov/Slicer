using UnityEngine;

namespace SA.Runtime.Core.Data
{
    [CreateAssetMenu(fileName = "SlicerMovementConfig", menuName = "SO/Configs/SlicerMovementConfig")]
    public class SlicerMovementConfig : ScriptableObject
    {
        [field: SerializeField, Min(1f)] public float ForwardForce {get; private set;} = 30f;
        [field: SerializeField, Min(1f)] public float RotateUpForce {get; private set;} = 100f;
        [field: SerializeField, Min(1f)] public float RotateDownForce {get; private set;} = 60f;
        [field: SerializeField, Min(1f)] public float ChangeRotateTimeMultiplier {get; private set;} = 50f;
        [field: SerializeField] public Vector2 RotationLimit {get; private set;} = new Vector2(-60f, 45f);
        [field: SerializeField] public bool IsCanRotate {get; private set;} = true;
    }
}