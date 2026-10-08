
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



#if FISHNETWORKED
using FishNet.Object.Synchronizing;
using SharedValues.Core.Collections;
using System.Collections.Generic;

namespace SharedValues.Networked
{
    public abstract class SharedNetDictionary<TKey, TValue, TReference> : SharedNetCollection<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>, TKey, TValue, TReference>
    where TReference : SharedDictionaryReference<TKey, TValue>
    {
        protected sealed override void AddNetworked(KeyValuePair<TKey, TValue> item)
        {
            AddNetworked(item.Key, item.Value);
        }

        protected sealed override void RemoveNetworked(KeyValuePair<TKey, TValue> item)
        {
            RemoveAtNetworked(item.Key);
        }

        protected void SetLocalValue(SyncDictionaryOperation op, TKey key, TValue value, bool asServer)
        {
            switch (op)
            {
                case SyncDictionaryOperation.Add:
                    LocalValue.Add(key, value);
                    break;
                case SyncDictionaryOperation.Remove:
                    LocalValue.RemoveAt(key);
                    break;
                case SyncDictionaryOperation.Set:
                    LocalValue[key] = value;
                    break;
                case SyncDictionaryOperation.Clear:
                    LocalValue.ResetCollection();
                    break;
            }
        }
    }
}
#endif
