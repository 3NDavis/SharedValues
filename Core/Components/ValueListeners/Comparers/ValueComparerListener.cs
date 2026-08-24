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
            onValueChangedConditionMet?.Invoke(met);
            onValueChangedConditionNotMet?.Invoke(!met);
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
