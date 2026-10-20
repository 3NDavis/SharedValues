
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

namespace SharedValues.Core.Collections
{
    public class SharedDictionary<TKey, TValue> : SharedCollection<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>, TKey, TValue>
    {

        protected override string GetTextureName()
        {
            return "Dictionary";
        }

        public sealed override TValue GetValue(TKey key)
        {
            return Value[key];
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

        public sealed override void AddWithoutNotify(TKey key, TValue value)
        {
            Value.Add(key, value);
        }

        public sealed override void AddWithoutNotify(KeyValuePair<TKey, TValue> collectionItem)
        {
            Value.Add(collectionItem.Key, collectionItem.Value);
        }

        public sealed override void RemoveAtWithoutNotify(TKey key)
        {
            Value.Remove(key);
        }

        public sealed override void RemoveWithoutNotify(KeyValuePair<TKey, TValue> collectionItem)
        {
            Value.Remove(collectionItem.Key);
        }

        public sealed override void SetValueWithoutNotify(TKey index, TValue value)
        {
            this.Value[index] = value;
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

        public sealed override TKey ConvertCollectionItemToKey(KeyValuePair<TKey, TValue> collectionItem)
        {
            return collectionItem.Key;
        }

        public sealed override TValue ConvertCollectionItemToValue(KeyValuePair<TKey, TValue> collectionItem)
        {
            return collectionItem.Value;
        }
    }


    public class SharedDictionaryReference<TKey, TValue> : SharedCollectionReference<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>, TKey, TValue>
    {
        public sealed override TValue GetValue(TKey key)
        {
            return Value[key];
        }

        public sealed override bool TryGetValue(TKey index, out TValue value)
        {
            return Value.TryGetValue(index, out value);
        }

        public sealed override void AddWithoutNotify(TKey key, TValue value)
        {
            Value.Add(key, value);
        }

        public sealed override void AddWithoutNotify(KeyValuePair<TKey, TValue> collectionItem)
        {
            Value.Add(collectionItem.Key, collectionItem.Value);
        }

        public sealed override void RemoveAtWithoutNotify(TKey key)
        {
            Value.Remove(key);
        }

        public sealed override void RemoveWithoutNotify(KeyValuePair<TKey, TValue> collectionItem)
        {
            Value.Remove(collectionItem.Key);
        }

        public sealed override void SetValueWithoutNotify(TKey index, TValue value)
        {
            Value[index] = value;
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

        public sealed override TKey ConvertCollectionItemToKey(KeyValuePair<TKey, TValue> collectionItem)
        {
            return collectionItem.Key;
        }

        public sealed override TValue ConvertCollectionItemToValue(KeyValuePair<TKey, TValue> collectionItem)
        {
            return collectionItem.Value;
        }
    }
}
