using System;

namespace SharedValues.Upgradable
{
    /// <summary>
    /// A shared value that has phenomenon modifiers applied to it
    /// </summary>
    // [CreateAssetMenu(menuName = "Shared Values/Upgradable/Value/Float", fileName = "RaRVal_Float_Name")]
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