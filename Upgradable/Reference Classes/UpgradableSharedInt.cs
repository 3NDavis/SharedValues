using System;

namespace SharedValues.Upgradable
{
    // [CreateAssetMenu(menuName = "Shared Values/Upgradable/Value/Int", fileName = "RaRVal_Int_Name")]
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