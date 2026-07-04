using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace SharedValues.Upgradable
{
    public abstract class ValueModifications<T, M>
    where T : struct where M : ValueModifier
    {
        [SerializeField] private M[] applicableModifiers;

        [Tooltip("<b>Playmode Only!</b> The value that the shared value reference is using, only updates when accessed.")]
        [AllowNesting, ReadOnly]
        [SerializeField] private T lastCalculatedValue;
        
        /// <summary>
        /// Applys all of the modifications to the value in and stores and returns them in the post modification value
        /// </summary>
        /// <param name="baseValue">the value to begin with for the modifications</param>
        /// <returns>The post modifications value</returns>
        public T ApplyModifications(T baseValue, Dictionary<ValueModifier, float> modificationsDomains)
        {
            T trackedValue = baseValue;

            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.Log("Should Check if any modifications have changed in order to not do calculations resulting in the same value");
            #endif

            for (int i = 0; i < applicableModifiers.Length; i++)
            {
                if(modificationsDomains.TryGetValue(applicableModifiers[i], out float modificationDomain))
                {
                    trackedValue = ApplySpecificModification(trackedValue, applicableModifiers[i], modificationDomain);
                }
            }

            lastCalculatedValue = trackedValue;

            return lastCalculatedValue;
        }

        protected abstract T ApplySpecificModification(T trackedValue, M valueModifier, float modificationDomain);
    }

}