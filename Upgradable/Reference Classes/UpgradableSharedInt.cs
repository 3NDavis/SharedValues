using System;

namespace SharedValues.Upgradable
{
    [Serializable]
    public class UpgradableSharedInt : UpgradableSharedValue<int, ValueModifier, IntValueModifications>
    {
    }
    [Serializable]
    public class IntValueModifications : ValueModifications<int, ValueModifier>
    {
        protected override int ApplySpecificModification(int trackedValue, ValueModifier valueModifier, float modificationDomain)
        {
            return valueModifier.GetModifiedInt(trackedValue, modificationDomain);
        }
    }
}