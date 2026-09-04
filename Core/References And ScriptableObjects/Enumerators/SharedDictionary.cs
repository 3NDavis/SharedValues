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
        protected override string GetTextureName()
        {
            return "Dictionary";
        }

        public override void ResetEnumerator()
        {
            if(Value == null)
            {
                Value = new Dictionary<TKey, TValue>();
            }
            else
            {
                Value.Clear();
            }
        }

        public override void AddToEnumerator(KeyValuePair<TKey, TValue> value)
        {
            Value.Add(value.key, value.value);
        }

        public override void RemoveFromEnumerator(KeyValuePair<TKey, TValue> value)
        {
            Value.Remove(value.key);
        }

        public override void SetEnumeratorValue(TKey index, KeyValuePair<TKey, TValue> value)
        {
            this.Value[index] = value.value;
        }

        public override bool TryGetEnumeratedValue(TKey index, out TValue value)
        {
            if (this.Value.ContainsKey(index))
            {
                value = this.Value[index];
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
        public override void AddToEnumerator(KeyValuePair<TKey, TValue> value)
        {
            Value.TryAdd(value.key, value.value);
            BroadcastToReference();
        }

        public override void AddToEnumeratorWithoutNotify(KeyValuePair<TKey, TValue> value)
        {
            Value.TryAdd(value.key, value.value);

        }

        public override void RemoveFromEnumerator(KeyValuePair<TKey, TValue> value)
        {
            if (Value.ContainsKey(value.key))
            {
                Value.Remove(value.key);
            }
            BroadcastToReference();
        }

        public override void RemoveFromEnumeratorWithoutNotify(KeyValuePair<TKey, TValue> value)
        {
            if (Value.ContainsKey(value.key))
            {
                Value.Remove(value.key);
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