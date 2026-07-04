using System.Collections.Generic;

namespace SharedValues.Enumerators
{
    public class KeyValuePair<TKey, TValue>
    {
        public TKey key;
        public TValue value;
    }

    public class SharedDictionary<TKey, TValue> : SharedEnumerator<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>, TValue, TKey>
    {
        public override void ResetEnumerator()
        {
            if(value == null)
            {
                SetValue(new Dictionary<TKey, TValue>());
            }
            else
            {
                value.Clear();
            }
        }

        public override void AddToEnumerator(KeyValuePair<TKey, TValue> item)
        {
            value.Add(item.key, item.value);
        }

        public override void RemoveFromEnumerator(KeyValuePair<TKey, TValue> item)
        {
            value.Remove(item.key);
        }

        public override void SetEnumeratorValue(TKey index, KeyValuePair<TKey, TValue> value)
        {
            this.value[index] = value.value;
        }

        public override bool TryGetEnumeratedValue(TKey index, out TValue value)
        {
            if (this.value.ContainsKey(index))
            {
                value = this.value[index];
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }
    }


    public class SharedDictionaryReference<TKey, TValue> : SharedEnumeratorReference<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>, TValue, TKey>
    {
        public override void AddToEnumerator(KeyValuePair<TKey, TValue> item)
        {
            Value.TryAdd(item.key, item.value);
            BroadcastToReference();
        }

        public override void AddToEnumeratorWithoutNotify(KeyValuePair<TKey, TValue> item)
        {
            Value.TryAdd(item.key, item.value);

        }

        public override void RemoveFromEnumerator(KeyValuePair<TKey, TValue> item)
        {
            if (Value.ContainsKey(item.key))
            {
                Value.Remove(item.key);
            }
            BroadcastToReference();
        }

        public override void RemoveFromEnumeratorWithoutNotify(KeyValuePair<TKey, TValue> item)
        {
            if (Value.ContainsKey(item.key))
            {
                Value.Remove(item.key);
            }
        }

        public override void ResetEnumerator()
        {
            if(Value == null)
                Value = new Dictionary<TKey, TValue>();
            else
                Value.Clear();

            BroadcastToReference();
        }

        public override void SetEnumeratedValue(TKey index, TValue value)
        {
            if(Value.ContainsKey(index))
                Value[index] = value;

            BroadcastToReference();
        }

        public override void SetEnumeratorValueWithoutNotify(TKey index, KeyValuePair<TKey, TValue> value)
        {
            if(Value.ContainsKey(index))
                Value[index] = value.value;
        }

        public override bool TryGetEnumeratatedValue(TKey index, out TValue value)
        {
            return Value.TryGetValue(index, out value);
        }
    }
}