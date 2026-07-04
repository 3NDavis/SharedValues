using System.Collections.Generic;

namespace SharedValues.Enumerators
{
    public class SharedList<TItem> : SharedEnumerator<List<TItem>, TItem, TItem, int>
    {
        public override void ResetEnumerator()
        {
            if(value == null)
            {
                SetValue(new List<TItem>());
            }
            else
            {
                value.Clear();
            }
        }

        public override void AddToEnumerator(TItem item)
        {
           value.Add(item);
        }

        public override void RemoveFromEnumerator(TItem item)
        {
            value.Remove(item);
        }

        public override void SetEnumeratorValue(int index, TItem item)
        {
            value[index] = item;
        }

        public override bool TryGetEnumeratedValue(int index, out TItem value)
        {
            if(index >= this.value.Count)
            {
                value = default;
                return false;
            }

            value = this.value[index];
            return true;
        }
    }

    public class SharedListReference<T> : SharedEnumeratorReference<List<T>, T, T, int>
    {
        public override void AddToEnumerator(T item)
        {
            AddToEnumeratorWithoutNotify(item);
            BroadcastToReference();
        }

        public override void AddToEnumeratorWithoutNotify(T item)
        {
            Value.Add(item);
        }

        public override void RemoveFromEnumerator(T item)
        {
            RemoveFromEnumeratorWithoutNotify(item);
            BroadcastToReference();
        }

        public override void RemoveFromEnumeratorWithoutNotify(T item)
        {
            Value.Remove(item);
        }

        public override void ResetEnumerator()
        {
            if(Value == null)
            {
                Value = new List<T>();
            }
            else
            {
                Value.Clear();
            }
            BroadcastToReference();
        }

        public override void SetEnumeratedValue(int index, T value)
        {
            SetEnumeratorValueWithoutNotify(index, value);

            BroadcastToReference();
        }

        public override void SetEnumeratorValueWithoutNotify(int index, T value)
        {
            if(index >= Value.Count)
                return;
            
            Value[index] = value;
        }

        public override bool TryGetEnumeratatedValue(int index, out T value)
        {
            if(Value.Count >= index)
            {
                value = default;
                return false;
            }

            value = Value[index];
            return true;
        }
    }
}