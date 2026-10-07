
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
   
   
   
using System.Collections;

namespace SharedValues.Core.Enumerators
{
    public interface ISharedCollection<TCollectionItem, TKey, TValue>
    {
        public int Count();
        public void Add(TCollectionItem collectionItem);
        public void Add(TKey key, TValue value);
        public void Remove(TCollectionItem collectionItem);
        public void RemoveAt(TKey key);
        public TValue GetValue(TKey key);
        public void SetValue(TKey key, TValue value);
        public bool TryGetValue(TKey key, out TValue value);
        public void ResetCollection();
    }

    public abstract class SharedCollection<TCollection, TCollectionItem, TKey, TValue> : SharedValue<TCollection>, ISharedCollection<TCollectionItem, TKey, TValue>
    where TCollection : ICollection, IEnumerable
    {      
        // indexer declaration
        public TValue this[TKey key]
        {
            get => GetValue(key);
            set => SetValue(key, value);
        }
        public int Count(){return Value.Count;}
        public abstract void Add(TCollectionItem collectionItem);
        public abstract void Add(TKey key, TValue value);
        public abstract void Remove(TCollectionItem collectionItem);
        public abstract void RemoveAt(TKey key);
        public abstract TValue GetValue(TKey key);
        public abstract void SetValue(TKey key, TValue value);
        public abstract bool TryGetValue(TKey key, out TValue value);
        public abstract void ResetCollection();
    }

    public abstract class SharedCollectionReference<TCollection, TCollectionItem, TKey, TValue> : SharedValueReference<TCollection>, ISharedCollection<TCollectionItem, TKey, TValue>
    where TCollection : ICollection, IEnumerable
    {
        public int Count(){return Value.Count;}
        public abstract void Add(TCollectionItem collectionItem);
        public abstract void Add(TKey key, TValue value);
        public abstract void Remove(TCollectionItem collectionItem);
        public abstract void RemoveAt(TKey key);
        public abstract TValue GetValue(TKey key);
        public abstract void SetValue(TKey key, TValue value);
        public abstract bool TryGetValue(TKey key, out TValue value);
        public abstract void ResetCollection();

        public abstract void AddWithoutNotify(TCollectionItem collectionItem);
        public abstract void AddWithoutNotify(TKey key, TValue value);
        public abstract void RemoveWithoutNotify(TCollectionItem collectionItem);
        public abstract void RemoveAtWithoutNotify(TKey key);
        public abstract void SetValueWithoutNotify(TKey key, TCollectionItem value);

        protected void BroadcastToReference()
        {
            switch (_ReferenceType)
            {
                case ReferenceType.global:
                    _SharedReference.BroadcastValueChange();
                    break;
                case ReferenceType.instanced:
                    SharedValue<TCollection> castSharedVal = (SharedValue<TCollection>)_instanceGroup.GetInstance(_SharedReference);
                    castSharedVal.BroadcastValueChange();
                    break;
            }
        }
    }
}
