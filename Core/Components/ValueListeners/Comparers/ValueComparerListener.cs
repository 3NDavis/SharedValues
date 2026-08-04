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

        [SerializeField] private T valueToCompareTo;

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

        [SerializeField] private UnityEvent onValueChangedConditionMet;
        [SerializeField] private UnityEvent onValueChangedConditionFail;
        
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
            if (met)
            {
                onValueChangedConditionMet?.Invoke();
            }
            else
            {
                onValueChangedConditionFail?.Invoke();
            }
        }

        private bool Compare(T newValue)
        {
            if((compareType & CompareType.equals) == CompareType.equals)
            {
                return newValue.Equals(valueToCompareTo);
            }
            if((compareType & CompareType.notEquals) == CompareType.notEquals)
            {
                return !newValue.Equals(valueToCompareTo);
            }
            return ComplexCompare(newValue, valueToCompareTo);
        }

        protected abstract bool ComplexCompare(T newValue, T compareValue);
    }
}
