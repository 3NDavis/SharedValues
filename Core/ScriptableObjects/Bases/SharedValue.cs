using System;
using UnityEngine;

namespace SharedValues
{
    public abstract class SharedValue : ScriptableObject
    {
        public abstract void BroadcastValueChange();
    }

    public abstract class SharedValue<T> : SharedValue
    {
        [field: SerializeField] public T value { get; private set; }
        public void SetValue(T newValue) { value = newValue; onValueChange?.Invoke(newValue); }
        public void SetValueWithoutNotify(T newValue) { value = newValue; }
        public event Action<T> onValueChange;

        //primarily for editor usage
        public override void BroadcastValueChange()
        {
            onValueChange?.Invoke(value);
        }

        protected virtual void OnDestroy()
        {
            if (onValueChange != null)
            {
                var invocationList = onValueChange.GetInvocationList();
                foreach (var invocation in invocationList)
                {
                    onValueChange -= (Action<T>)invocation;
                }
            }
        }
    }
}