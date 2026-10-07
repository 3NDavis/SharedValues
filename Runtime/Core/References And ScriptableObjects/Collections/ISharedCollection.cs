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



using System;

namespace SharedValues.Core.Collections
{
    public interface ISharedCollection<TCollectionItem, TKey, TValue>
    {
#region  Get
        public int Count();
        public TValue GetValue(TKey key);
        public bool TryGetValue(TKey key, out TValue value);
#endregion

#region  Set Abstract
        public void AddWithoutNotify(TKey key, TValue value);
        public void AddWithoutNotify(TCollectionItem collectionItem);
        public void RemoveAtWithoutNotify(TKey key);
        public void RemoveWithoutNotify(TCollectionItem collectionItem);
        public void SetValueWithoutNotify(TKey key, TValue value);
        public TKey ConvertCollectionItemToKey(TCollectionItem collectionItem);
        public TValue ConvertCollectionItemToValue(TCollectionItem collectionItem);
        public void ResetCollection();
#endregion

#region  Set
        public void Add(TCollectionItem collectionItem)
        {
            Add(ConvertCollectionItemToKey(collectionItem), ConvertCollectionItemToValue(collectionItem));
        }
        public void Add(TKey key, TValue value)
        {
            AddWithoutNotify(key, value);
            OnCollectionChange(CollectionModificationType.Add, key, value);
        }
        public void Remove(TCollectionItem collectionItem)
        {
            RemoveAt(ConvertCollectionItemToKey(collectionItem));
        }
        public void RemoveAt(TKey key)
        {
            RemoveAtWithoutNotify(key);
            OnCollectionChange(CollectionModificationType.Remove, key, default);
        }
        public void SetCollectionValue(TKey key, TValue value)
        {
            SetValueWithoutNotify(key, value);
            OnCollectionChange(CollectionModificationType.Add, key, value);
        }
        public void ClearCollection()
        {
            ResetCollection();
            OnCollectionChange(CollectionModificationType.Clear, default, default);
        }
#endregion

#region  Broadcast
        public void AddListener(Action<CollectionModificationType, TKey, TValue> action);
        public void RemoveListener(Action<CollectionModificationType, TKey, TValue> action);
        public void OnCollectionChange(CollectionModificationType modificationType, TKey key, TValue value);
        // {
        //     onCollectionModified?.Invoke(modificationType, key, value);
        // }
#endregion
    }
}