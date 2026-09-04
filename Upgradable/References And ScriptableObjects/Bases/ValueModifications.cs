
   //Copyright 2026 Ethan Davis

   //Licensed under the Apache License, Version 2.0 (the "License");
   //you may not use this file except in compliance with the License.
   //You may obtain a copy of the License at
   //  http://www.apache.org/licenses/LICENSE-2.0

   //Unless required by applicable law or agreed to in writing, software
   //distributed under the License is distributed on an "AS IS" BASIS,
   //WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   //See the License for the specific language governing permissions and
   //limitations under the License.
   
   
   
using System.Collections.Generic;
using UnityEngine;

namespace SharedValues.Upgradable
{
    public abstract class ValueModifications<T, M>
    where T : struct where M : ValueModifier
    {
        [SerializeField] private M[] applicableModifiers;

        [Tooltip("<b>Playmode Only!</b> The value that the shared value reference is using, only updates when accessed.")]
        private T lastCalculatedValue;
        
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

            if(modificationsDomains == null)
                return baseValue;

            if(applicableModifiers.Length == 0)
            {
                return baseValue;
            }

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
