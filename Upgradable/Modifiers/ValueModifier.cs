using System;
using UnityEngine;

//To see a visual representation of the values go here:
//https://www.desmos.com/calculator/qhvwgqb3bf

namespace SharedValues.Upgradable
{
    [CreateAssetMenu(menuName = "Shared Values/Upgradable/Value Modifer/Float", fileName = "SharedValMod_Float_Name")]
    public class ValueModifier : ScriptableObject
    {
        [NonSerialized] private float lastDomainUsed;
        [NonSerialized] private float lastValueIn;
        [NonSerialized] private float lastValueOut;

        [Header("Parameters")]
        [SerializeField] private float minDomain = 0;
        public float MinDomain => minDomain;
        [SerializeField] private float maxDomain = 2;
        public float MaxDomain => maxDomain;

        /// <summary> The default value of the modification, for bools 0 is false, 1 is true </summary>
        [SerializeField] private float defaultDomainValue = 1;
        public float DefaultValue => defaultDomainValue;

        /// <summary>
        /// The ammount a slider with a custom step will move the slider on navigate
        /// </summary>
        [Tooltip("The ammount a slider with a custom step will move the slider on navigate")]
        [SerializeField] private float stepValue = 0.1f;
        public float StepValue => stepValue; 

        [SerializeField] private ModificationType modificationType = ModificationType.addDomain;

        [SerializeField] private float sCurveSlope = 10;

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
                case ModificationType.addDomain:
                    return baseValue + modificationDomain;

                case ModificationType.multiplyDomain:
                    return baseValue * modificationDomain;

                case ModificationType.inPowerOfDomain:
                    return Mathf.Pow(baseValue, modificationDomain);

                case ModificationType.domainPowerOfIn:
                    return Mathf.Pow(modificationDomain, baseValue);

                case ModificationType.inLogOfDomain:
                    return Mathf.Log(baseValue, modificationDomain);

                case ModificationType.domainLogOfIn:
                    return Mathf.Log(modificationDomain, baseValue);

                case ModificationType.replaceWithDomain:
                    return modificationDomain;

                case ModificationType.sCurve:
                    return SCurve.Evaluate(minDomain, maxDomain, baseValue, modificationDomain, sCurveSlope);

                case ModificationType.zCurve:
                    return SCurve.Evaluate(minDomain, maxDomain, baseValue, modificationDomain, sCurveSlope, true);

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
            addDomain,
            /// <summary>
            /// Multiplies the modification value to the target value
            /// </summary>
            multiplyDomain,
            /// <summary>
            /// Raises the target value to the power of the modification value
            /// </summary>
            inPowerOfDomain,
            /// <summary>
            /// Raises the modification value to the power of the target value
            /// </summary>
            domainPowerOfIn,
            /// <summary>
            /// Performs a logarithm using the modification's value as the base to the target value
            /// </summary>
            inLogOfDomain,
            /// <summary>
            /// Performs a logarithm using the target's value as the base to the modification value
            /// </summary>
            domainLogOfIn,
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
            replaceWithDomain,
        }
    }
}