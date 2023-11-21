
using UnityEngine;

namespace SA.Runtime.Core.Pickup
{
    public interface IPickupItem
    {
        Transform MyTransform {get;}
        void Pickup();
        void Reclaim();
    }
}