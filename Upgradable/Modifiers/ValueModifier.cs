using UnityEngine;
using Toolbox;
using NaughtyAttributes;

//To see a visual representation of the values go here:
//https://www.desmos.com/calculator/qhvwgqb3bf

namespace SharedValues.Upgradable
{
    [CreateAssetMenu(menuName = "Shared Values/Upgradable/Value Modifer/Float", fileName = "SharedValMod_Float_Name")]
    public class ValueModifier : ScriptableObject
    {
        [Header("Tooltip")]
        [SerializeField] private Tooltip tooltip;
        public Tooltip Tooltip => tooltip;

        public enum PriorityGroup
        {
            //order from apply first at top, to apply last at bottom
            levelUp ,
            pickup,
            phenomenon
        }

        [Space]
        [AllowNesting, ReadOnly]
        [SerializeField] private float lastDomainUsed;
        [AllowNesting, ReadOnly]
        [SerializeField] private float lastValueIn;
        [AllowNesting, ReadOnly]
        [SerializeField] private float lastValueOut;

        [Header("Parameters")]
        [AllowNesting, InfoBox("For visual curves see: https://www.desmos.com/calculator/qhvwgqb3bf")]
        [SerializeField] private float minValue = 0;
        public float MinValue => minValue;
        [SerializeField] private float maxValue = 2;
        public float MaxValue => maxValue;

        /// <summary> The default value of the modification, for bools 0 is false, 1 is true </summary>
        [SerializeField] private float defaultDomainValue = 1;
        public float DefaultValue => defaultDomainValue;
        /// <summary> The default value of the slider, for bools 0 is false, 1 is true </summary>
        /// <summary>
        /// The ammount a slider with a custom step will move the slider on navigate
        /// </summary>
        [SerializeField] private float stepValue = 0.1f;
        public float StepValue => stepValue; 

        [SerializeField] private ModificationType modificationType = ModificationType.additive;

        bool showSCurve => modificationType == ModificationType.sCurve || modificationType == ModificationType.zCurve;
        [ShowIf("showSCurve")]
        [SerializeField] private float sCurveSlope = 10;

        [ShowIf("modificationType", ModificationType.customCurve)]
        [SerializeField] private AnimationCurve curve;

        public bool CheckLastDomain(float newValueIn, float newDomain, out float lastValueOut)
        {
            if(newDomain == lastDomainUsed & newValueIn == lastValueIn)
            {
                lastValueOut = this.lastValueOut;
                return true;
            }
            lastValueOut = default;
            return false;
        }

//To see a visual representation of the values go here:
//https://www.desmos.com/calculator/qhvwgqb3bf

/// <param name="baseValue"></param>
/// <returns>A Modified float based on the modification Type</returns>
        public float GetModifiedFloat(float baseValue, float modificationDomain)
        {
            if (CheckLastDomain(baseValue, modificationDomain, out lastValueOut))
            {
                return lastValueOut;
            }

            lastValueOut = DoFunctionWithDomain(baseValue, modificationDomain);
            lastValueIn = baseValue;
            lastDomainUsed = modificationDomain;
            return lastValueOut;
        }

        private float DoFunctionWithDomain(float baseValue, float modificationDomain)
        {
            switch (modificationType)
            {
                case ModificationType.additive:
                    return baseValue + modificationDomain;

                case ModificationType.multiplicative:
                    return baseValue * modificationDomain;

                case ModificationType.exponential:
                    return Mathf.Pow(baseValue, modificationDomain);

                case ModificationType.inverseExponential:
                    return Mathf.Pow(modificationDomain, baseValue);

                case ModificationType.logarithmic:
                    return Mathf.Log(baseValue, modificationDomain);

                case ModificationType.inverseLogarithmic:
                    return Mathf.Log(modificationDomain, baseValue);

                case ModificationType.replace:
                    return modificationDomain;

                case ModificationType.sCurve:
                    return SCurve.Evaluate(minValue, maxValue, baseValue, modificationDomain, sCurveSlope);

                case ModificationType.zCurve:
                    return SCurve.Evaluate(minValue, maxValue, baseValue, modificationDomain, sCurveSlope, true);

                case ModificationType.customCurve:
                    return curve.Evaluate(modificationDomain);

                default:
                    return modificationDomain;
            }
        }



        /// <param name="trackedValue"></param>
        /// <returns>A modified integer based on the modification type</returns>
        public int GetModifiedInt(int trackedValue, float modificationDomain)
        {
            float fValue = trackedValue;
            fValue = GetModifiedFloat(fValue, modificationDomain);
            return (int)fValue;
        }

        /// <summary>
        /// Determines how the modification applies to the target value
        /// </summary>
        /// 
//To see a visual representation of the modifications go here:
//https://www.desmos.com/calculator/qhvwgqb3bf
        private enum ModificationType
        {
            /// <summary>
            /// Adds the modification value to the target value
            /// </summary>
            additive,
            /// <summary>
            /// Multiplies the modification value to the target value
            /// </summary>
            multiplicative,
            /// <summary>
            /// Raises the target value to the power of the modification value
            /// </summary>
            exponential,
            /// <summary>
            /// Raises the modification value to the power of the target value
            /// </summary>
            inverseExponential,
            /// <summary>
            /// Performs a logarithm using the modification's value as the base to the target value
            /// </summary>
            logarithmic,
            /// <summary>
            /// Performs a logarithm using the target's value as the base to the modification value
            /// </summary>
            inverseLogarithmic,
            /// <summary>
            /// Performs a calculation starting exponential which turns logarithmic at the target value 
            /// </summary>
            sCurve,
            /// <summary>
            /// Performs a calculation starting logarithmic which turns exponential at the target value 
            /// </summary>
            zCurve,
            /// <summary>
            /// Replaces the target with the curve value a x = modification value
            /// </summary>
            customCurve,
            /// <summary>
            /// Replaces the target value with the modification's value
            /// </summary>
            replace,
        }
    }
}