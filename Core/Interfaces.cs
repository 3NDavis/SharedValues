using System;

namespace SharedValues
{
    public interface IValueEventHandler<T>
    {
        public void AddListener(Action<T> action);
        public void RemoveListener(Action<T> action);
    }

    public interface IValueSetter<T>
    {
        public T Value {get; set;}
    }

    public interface IValueSpecialSetter<T> : IValueSetter<T>
    {
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
    }
}