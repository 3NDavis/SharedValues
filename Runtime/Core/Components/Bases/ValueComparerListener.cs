
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
   
   
   
using System;
using UnityEngine;
using UnityEngine.Events;

namespace SharedValues.Core
{
    /// <summary>
    /// The base class for components that compares between SharedValueReferences when the valueToListenTo broadcasts a value change
    /// </summary>
    /// <typeparam name="T">The type of SharedValue</typeparam>
    /// <typeparam name="SVR">The SharedValueReference of type <typeparamref name="T"/></typeparam>
    public abstract class ValueComparerListener<T, SVR> : MonoBehaviour
    where T : struct, IComparable where SVR : SharedValueReference<T>
    {
        [SerializeField] private SVR valueToCompareTo;

        [SerializeField] private CompareType compareType;
        protected CompareType p_compareType => compareType;

        [Flags]
        protected enum CompareType
        {
            equals = 1,
            notEquals = 2,
            greater = 4,
            less = 8
        }

        [Tooltip("Will broadcast only the oppropriate message for the condition. Otherwise, both are brodcasted")]
        [SerializeField] private bool broadcastConditionally;

        [SerializeField] private UnityEvent<bool> onValueChangedConditionMet;
        [SerializeField] private UnityEvent<bool> onValueChangedConditionNotMet;
        
        public void CompareToStoredValue(T newValue)
        {
            bool met = Compare(newValue);
            if (broadcastConditionally)
            {
                if (met)
                {
                    onValueChangedConditionMet?.Invoke(met);
                }
                else
                {
                    onValueChangedConditionNotMet?.Invoke(!met);
                }
            }
            else
            {
                onValueChangedConditionMet?.Invoke(met);
                onValueChangedConditionNotMet?.Invoke(!met);
            }
        }

        /// <summary>
        /// Compares the <paramref name="newValue"/> to the valueToCompareTo using the compareType
        /// </summary>
        /// <returns>The result of the comparison</returns>
        private bool Compare(T newValue)
        {
            if((compareType & CompareType.equals) == CompareType.equals)
            {
                if(newValue.Equals(valueToCompareTo.Value))
                    return true;
            }
            if((compareType & CompareType.notEquals) == CompareType.notEquals)
            {
                if(!newValue.Equals(valueToCompareTo.Value))
                    return true;
            }
            if(ComplexCompare(newValue, valueToCompareTo.Value))
                return true;
            
            return false;
        }

        /// <summary>
        /// Compares the to values using < or > 
        /// </summary>
        /// <returns></returns>
        protected abstract bool ComplexCompare(T newValue, T compareValue);

#if UNITY_EDITOR
        //this updates the inspector only value for the shared value
        //since its inspector only, it shouldn't be compiled in builds
        protected virtual void Update()
        {
            var b = valueToCompareTo.Value;
        }
#endif
    }
}

