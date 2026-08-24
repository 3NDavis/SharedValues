using System;

namespace SharedValues
{
    internal interface IValueListener<T>
    {
        public T Value {get; set;}
        public void SetValueWithoutNotify(T value);
        public void SetValueWithoutNotify(SharedValue<T> value)
        {
            SetValueWithoutNotify(value.Value);
        }
        public void SetValueWithBroadcastIfChange(T value)
        {
            if(!value.Equals(Value))
            {
                Value = value;
            }
        }
        public void SetValueWithBroadcastIfChange(SharedValue<T> value)
        {
            SetValueWithBroadcastIfChange(value.Value);
        }
        public void AddListener(Action<T> action);
        public void RemoveListener(Action<T> action);
    }
}