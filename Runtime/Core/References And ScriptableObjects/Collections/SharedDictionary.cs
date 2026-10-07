
//Copyright 2026 Ethan Davis

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//  http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.



using System.Collections.Generic;

namespace SharedValues.Core.Enumerators
{
    public class SharedDictionary<TKey, TValue> : SharedCollection<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>, TKey, TValue>
    {

        protected override string GetTextureName()
        {
            return "Dictionary";
        }

        public sealed override void ResetCollection()
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

        public sealed override void Add(KeyValuePair<TKey, TValue> collectionItem)
        {
            Value.Add(collectionItem.Key, collectionItem.Value);
        }

        public sealed override void Add(TKey key, TValue value)
        {
            Value.Add(key, value);
        }

        public sealed override void Remove(KeyValuePair<TKey, TValue> value)
        {
            RemoveAt(value.Key);
        }

        public sealed override void RemoveAt(TKey key)
        {
            Value.Remove(key);
        }

        public sealed override TValue GetValue(TKey key)
        {
            return Value[key];
        }

        public sealed override void SetValue(TKey index, TValue value)
        {
            this.Value[index] = value;
        }

        public sealed override bool TryGetValue(TKey index, out TValue value)
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


    public class SharedDictionaryReference<TKey, TValue> : SharedCollectionReference<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>, TKey, TValue>
    {
        public sealed override void Add(KeyValuePair<TKey, TValue> collectionItem)
        {
            Value.TryAdd(collectionItem.Key, collectionItem.Value);
            BroadcastToReference();
        }

        public sealed override void Add(TKey key, TValue value)
        {
            Value.TryAdd(key, value);
            BroadcastToReference();
        }

        public sealed override void Remove(KeyValuePair<TKey, TValue> value)
        {
            RemoveAt(value.Key);
        }

        public override void RemoveAt(TKey key)
        {
            if (Value.ContainsKey(key))
            {
                Value.Remove(key);
            }
            BroadcastToReference();
        }

        public sealed override void ResetCollection()
        {
            if(Value == null)
                Value = new Dictionary<TKey, TValue>();
            else
                Value.Clear();

            BroadcastToReference();
        }

        public sealed override TValue GetValue(TKey key)
        {
            return Value[key];
        }

        public sealed override void SetValue(TKey index, TValue value)
        {
            if(Value.ContainsKey(index))
                Value[index] = value;

            BroadcastToReference();
        }

        public sealed override void AddWithoutNotify(KeyValuePair<TKey, TValue> value)
        {
            Value.TryAdd(value.Key, value.Value);

        }

        public sealed override void AddWithoutNotify(TKey key, TValue value)
        {
            Value.TryAdd(key, value);
            BroadcastToReference();
        }

        public sealed override void RemoveWithoutNotify(KeyValuePair<TKey, TValue> value)
        {
            RemoveAtWithoutNotify(value.Key);
        }

        public override void RemoveAtWithoutNotify(TKey key)
        {
            if (Value.ContainsKey(key))
            {
                Value.Remove(key);
            }
        }

        public sealed override void SetValueWithoutNotify(TKey index, KeyValuePair<TKey, TValue> value)
        {
            if(Value.ContainsKey(index))
                Value[index] = value.Value;
        }

        public sealed override bool TryGetValue(TKey index, out TValue value)
        {
            return Value.TryGetValue(index, out value);
        }
    }
}
