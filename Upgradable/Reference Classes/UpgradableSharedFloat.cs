using System;

namespace SharedValues.Upgradable
{
    [Serializable]
    public class UpgradableSharedFloat : UpgradableSharedValue<float, ValueModifier, FloatValueModifications>
    {
    }
    [Serializable]
    public class FloatValueModifications : ValueModifications<float, ValueModifier>
    {
        protected override float ApplySpecificModification(float trackedValue, ValueModifier valueModifier, float modificationDomain)
        {
            return valueModifier.GetModifiedFloat(trackedValue, modificationDomain);
        }
    }

}