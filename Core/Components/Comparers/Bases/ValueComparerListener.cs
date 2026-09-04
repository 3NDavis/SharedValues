
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

namespace SharedValues
{
    public abstract class ValueComparerListener<T, R> : MonoBehaviour
    where T : struct, IComparable where R : SharedValueReference<T>
    {
        ///<summary>The value that is listened to to invoke the event</summary>
        [Tooltip("The value that is listened to to invoke the event")]
        [SerializeField] private R value;

        [SerializeField] private R valueToCompareTo;

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

        [Tooltip("Will broadcast only the oppropriate message for IsOwner. Otherwise, both are brodcasted")]
        [SerializeField] private bool broadcastConditionally;

        [SerializeField] private UnityEvent<bool> onValueChangedConditionMet;
        [SerializeField] private UnityEvent<bool> onValueChangedConditionNotMet;
        
        void OnEnable()
        {
            value.AddListener(BroadcastIsConditionMet);
        }

        void Start()
        {
            BroadcastIsConditionMet(value.Value);
        }

        void OnDisable()
        {
            value.RemoveListener(BroadcastIsConditionMet);
        }

        private void BroadcastIsConditionMet(T newValue)
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

        public void BroadcastIsConditionMet()
        {
            BroadcastIsConditionMet(value.Value);
        }

        private bool Compare(T newValue)
        {
            if((compareType & CompareType.equals) == CompareType.equals)
            {
                return newValue.Equals(valueToCompareTo.Value);
            }
            if((compareType & CompareType.notEquals) == CompareType.notEquals)
            {
                return !newValue.Equals(valueToCompareTo.Value);
            }
            return ComplexCompare(newValue, valueToCompareTo.Value);
        }

        protected abstract bool ComplexCompare(T newValue, T compareValue);
    }
}

