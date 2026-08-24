using System.Collections.Generic;

namespace SharedValues.Enumerators
{
    public class SharedList<TItem> : SharedEnumerator<List<TItem>, TItem, TItem, int>
    {
        public override void ResetEnumerator()
        {
            if(Value == null)
            {
                Value = new List<TItem>();
            }
            else
            {
                Value.Clear();
            }
        }

        public override void AddToEnumerator(TItem value)
        {
           Value.Add(value);
        }

        public override void RemoveFromEnumerator(TItem value)
        {
            Value.Remove(value);
        }

        public override void SetEnumeratorValue(int index, TItem value)
        {
            Value[index] = value;
        }

        public override bool TryGetEnumeratedValue(int index, out TItem value)
        {
            if(index >= this.Value.Count)
            {
                value = default;
                return false;
            }

            value = this.Value[index];
            return true;
        }
    }

    public class SharedListReference<T> : SharedEnumeratorReference<List<T>, T, T, int>
    {
        public override void AddToEnumerator(T value)
        {
            AddToEnumeratorWithoutNotify(value);
            BroadcastToReference();
        }

        public override void AddToEnumeratorWithoutNotify(T value)
        {
            Value.Add(value);
        }

        public override void RemoveFromEnumerator(T value)
        {
            RemoveFromEnumeratorWithoutNotify(value);
            BroadcastToReference();
        }

        public override void RemoveFromEnumeratorWithoutNotify(T value)
        {
            Value.Remove(value);
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