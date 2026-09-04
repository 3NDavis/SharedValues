using UnityEngine;

namespace SharedValues.Upgradable
{
    [CreateAssetMenu(menuName = "Shared Values/Upgradable/Value Modifer/Vector2", fileName = "SharedValMod_V2_Name")]
    public class VectorValueModifier : ValueModifier
    {
        protected override string GetTextureName()
        {
            return "mV2";
        }
        
        [SerializeField] VectorValue vectorVariableToModify = VectorValue.Magnitude;

/// <param name="baseValue"></param>
/// <returns>A vector with the same direction and a modified magnitude based on the modification type</returns>
        public Vector2 GetModifiedVector2(Vector2 baseValue, float modificationDomain, bool forceMagnitude = false)
        {
            if (forceMagnitude)
            {
                float magnitude = baseValue.magnitude;
                magnitude = GetModifiedFloat(magnitude, modificationDomain);

                return baseValue.normalized * magnitude;
            }
                
            switch (vectorVariableToModify)
            {
                case VectorValue.Magnitude:
                    float magnitude = baseValue.magnitude;
                    magnitude = GetModifiedFloat(magnitude, modificationDomain);

                    return baseValue.normalized * magnitude;

                case VectorValue.X:
                    float x = baseValue.x;
                    x = GetModifiedFloat(x, modificationDomain);
                    return new Vector2(x, baseValue.y);

                case VectorValue.y:
                    float y = baseValue.y;
                    y = GetModifiedFloat(y, modificationDomain);
                    return new Vector2(baseValue.x, y);

                case VectorValue.XY:
                    float y1 = baseValue.y;
                    y1 = GetModifiedFloat(y1, modificationDomain);
                    float x1 = baseValue.x;
                    x1 = GetModifiedFloat(x1, modificationDomain);
                    return new Vector2(x1, y1);
                
                default:
                    return baseValue;
            }
        }

        private enum VectorValue
        {
            X,
            y,
            XY,
            Magnitude
        }
    }
}