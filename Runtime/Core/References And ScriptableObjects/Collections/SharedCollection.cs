
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
using System.Collections;

namespace SharedValues.Core.Collections
{
    public enum CollectionModificationType
    {
        Clear,
        Add,
        Remove,
        Set,
    }

    public abstract class SharedCollection<TCollection, TCollectionItem, TKey, TValue> : SharedValue<TCollection>, ISharedCollection<TCollectionItem, TKey, TValue>
    where TCollection : ICollection, IEnumerable
    {
        // indexer declaration
        public TValue this[TKey key]
        {
            get => GetValue(key);
            set {SetValueWithoutNotify(key, value); OnCollectionChange(CollectionModificationType.Set, key, value);}
        }
        public int Count(){return Value.Count;}
        public abstract TValue GetValue(TKey key);
        public abstract bool TryGetValue(TKey key, out TValue value);

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

        public abstract void AddWithoutNotify(TKey key, TValue value);
        public abstract void AddWithoutNotify(TCollectionItem collectionItem);
        public abstract void RemoveAtWithoutNotify(TKey key);
        public abstract void RemoveWithoutNotify(TCollectionItem collectionItem);
        public abstract void SetValueWithoutNotify(TKey key, TValue value);
        public abstract TKey ConvertCollectionItemToKey(TCollectionItem collectionItem);
        public abstract TValue ConvertCollectionItemToValue(TCollectionItem collectionItem);
        public abstract void ResetCollection();

        public event Action<CollectionModificationType, TKey, TValue> onCollectionModified;
        public void AddListener(Action<CollectionModificationType, TKey, TValue> action)
        {
            onCollectionModified += action;
        }

        public void RemoveListener(Action<CollectionModificationType, TKey, TValue> action)
        {
            onCollectionModified += action;
        }
        public void OnCollectionChange(CollectionModificationType modificationType, TKey key, TValue value)
        {
            onCollectionModified?.Invoke(modificationType, key, value);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (onCollectionModified != null)
            {
                var invocationList = onCollectionModified.GetInvocationList();
                foreach (var invocation in invocationList)
                {
                    onCollectionModified -= (Action<CollectionModificationType, TKey, TValue>)invocation;
                }
            }
        }
    }

    public abstract class SharedCollectionReference<TCollection, TCollectionItem, TKey, TValue> : SharedValueReference<TCollection>, ISharedCollection<TCollectionItem, TKey, TValue>
    where TCollection : ICollection, IEnumerable
    {
        protected internal SharedCollection<TCollection, TCollectionItem, TKey, TValue> SharedCollection => 
            (SharedCollection<TCollection, TCollectionItem, TKey, TValue>)SharedValue;

        // indexer declaration
        public TValue this[TKey key]
        {
            get => GetValue(key);
            set {SetValueWithoutNotify(key, value); OnCollectionChange(CollectionModificationType.Set, key, value);}
        }
        public int Count(){return Value.Count;}
        public abstract TValue GetValue(TKey key);
        public abstract bool TryGetValue(TKey key, out TValue value);
        
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


        public abstract void AddWithoutNotify(TKey key, TValue value);
        public abstract void AddWithoutNotify(TCollectionItem collectionItem);
        public abstract void RemoveAtWithoutNotify(TKey key);
        public abstract void RemoveWithoutNotify(TCollectionItem collectionItem);
        public abstract void SetValueWithoutNotify(TKey key, TValue value);
        public abstract void ResetCollection();
        public abstract TKey ConvertCollectionItemToKey(TCollectionItem collectionItem);
        public abstract TValue ConvertCollectionItemToValue(TCollectionItem collectionItem);

        public void AddListener(Action<CollectionModificationType, TKey, TValue> action)
        {
            switch (_ReferenceType)
            {
                case ReferenceType.global:
                    SharedCollection.AddListener(action);
                    break;
                case ReferenceType.instanced:
                    var castSharedVal = InstanceGroup.GetInstance(SharedCollection);
                    castSharedVal.AddListener(action);
                    break;
            }        
        }

        public void RemoveListener(Action<CollectionModificationType, TKey, TValue> action)
        {
            switch (_ReferenceType)
            {
                case ReferenceType.global:
                    SharedCollection.RemoveListener(action);
                    break;
                case ReferenceType.instanced:
                    var castSharedVal = InstanceGroup.GetInstance(SharedCollection);
                    castSharedVal.RemoveListener(action);
                    break;
            }
        }

        public void OnCollectionChange(CollectionModificationType modificationType, TKey key, TValue value)
        {
            switch (_ReferenceType)
            {
                case ReferenceType.global:
                    SharedCollection.OnCollectionChange(modificationType, key, value);
                    SharedValue.BroadcastValueChange();
                    break;
                case ReferenceType.instanced:
                    var castSharedVal = InstanceGroup.GetInstance(SharedCollection);
                    castSharedVal.OnCollectionChange(modificationType, key, value);
                    castSharedVal.BroadcastValueChange();
                    break;
            }
        }
    }
}
