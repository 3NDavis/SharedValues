using System;
using UnityEngine;

namespace SharedValues
{
    public abstract class SharedValue : SharedSOBase
    {
        public abstract void BroadcastValueChange();

        protected override string GetFilePath()
        {
            return k_sharedValueFilePath + "Core\\";
        }
    }

    public abstract class SharedValue<T> : SharedValue, IValueSpecialSetter<T>, IValueEventHandler<T>
    {
        [SerializeField] private T value;
        public T Value { get => value; set => SetValue(value); }
        private void SetValue(T newValue) { value = newValue; onValueChange?.Invoke(newValue); }
        public void SetValue(SharedValue<T> newValue) {SetValue(newValue.value);}
        public void SetToDefault(){Value = default;}
        public void SetValueWithoutNotify(T newValue) { value = newValue; }
        public void SetValueWithoutNotify(SharedValue<T> newValue) { SetValueWithoutNotify(newValue.value); }
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

        public void AddListener(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public void RemoveListener(Action<T> action)
        {
            throw new NotImplementedException();
        }
    }
}