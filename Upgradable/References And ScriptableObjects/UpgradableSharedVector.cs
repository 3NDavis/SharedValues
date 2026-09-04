using System;
using UnityEngine;

namespace SharedValues.Upgradable
{
    [Serializable]
    public class UpgradableSharedVector : UpgradableSharedValue<Vector2, VectorValueModifier, VectorValueModifications>
    {
        
    }

    [Serializable]
    public class VectorValueModifications : ValueModifications<Vector2, VectorValueModifier>
    {
        protected override Vector2 ApplySpecificModification(Vector2 trackedValue, VectorValueModifier valueModifier, float modificationDomain)
        {
            return valueModifier.GetModifiedVector2(trackedValue, modificationDomain);
        }
    }

}