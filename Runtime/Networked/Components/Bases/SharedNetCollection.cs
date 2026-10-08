
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
using SharedValues.Core.Collections;
using System;
using System.Collections;

namespace SharedValues.Networked
{
    public abstract class SharedNetCollection<TCollection, TCollectionItem, TKey, TValue, TReference> : SharedNetValue<TCollection, TReference>, ISharedCollection<TCollectionItem, TKey, TValue>
    where TCollection : ICollection, IEnumerable
    where TReference : SharedCollectionReference<TCollection, TCollectionItem, TKey, TValue>, ISharedCollection<TCollectionItem,TKey,TValue>
    {
        protected override void OnEnable()
        {
#if UNITY_EDITOR
            LocalValue.AddListener(UpdateCollection);
#else
            if (listenToLocalValue)
            {
                LocalValue.AddListener(UpdateCollection);
            }
#endif
        }
        protected override void OnDisable()
        {
            LocalValue.RemoveListener(UpdateCollection);
        }

        private void UpdateCollection(CollectionModificationType type, TKey key, TValue value)
        {
#if UNITY_EDITOR
            if(localInteractionMethod != LocalInteractionMethod.listen) 
                return;
#endif

            if(checkForOwnership & !IsOwner) 
                return;

            switch (type)
            {
                case CollectionModificationType.Clear:
                    ResetNetworked();
                    break;
                case CollectionModificationType.Add:
                    AddNetworked(key, value);
                    break;
                case CollectionModificationType.Remove:
                    RemoveAtNetworked(key);
                    break;
                case CollectionModificationType.Set:
                    SetValueNetworked(key, value);
                    break;
            }
        }


        // indexer declaration
        public TValue this[TKey key]
        {
            get => GetValue(key);
            set => SetValueWithoutNotify(key, value);
        }

        public void Add(TCollectionItem collectionItem)
        {
            if(checkForOwnership & !IsOwner)
                return;
                
            Add(ConvertCollectionItemToKey(collectionItem), ConvertCollectionItemToValue(collectionItem));
        }
        public void Add(TKey key, TValue value)
        {
            if(checkForOwnership & !IsOwner)
                return;
                
            AddWithoutNotify(key, value);
            OnCollectionChange(CollectionModificationType.Add, key, value);
        }
        public void Remove(TCollectionItem collectionItem)
        {
            if(checkForOwnership & !IsOwner)
                return;
                
            RemoveAt(ConvertCollectionItemToKey(collectionItem));
        }
        public void RemoveAt(TKey key)
        {
            if(checkForOwnership & !IsOwner)
                return;
                
            RemoveAtWithoutNotify(key);
            OnCollectionChange(CollectionModificationType.Remove, key, default);
        }
        public void SetCollectionValue(TKey key, TValue value)
        {
            if(checkForOwnership & !IsOwner)
                return;
                
            SetValueWithoutNotify(key, value);
            OnCollectionChange(CollectionModificationType.Add, key, value);
        }
        public void ClearCollection()
        {
            if(checkForOwnership & !IsOwner)
                return;

            ResetCollection();
            OnCollectionChange(CollectionModificationType.Clear, default, default);
        }

        //this attribute should be put over all the ___Networked() functions
        //require ownership is false since that check is already done in SetValue();
        // [ServerRpc(RequireOwnership = false, RunLocally = false)] 
        protected abstract void AddNetworked(TCollectionItem item);
        protected abstract void AddNetworked(TKey key, TValue value);
        protected abstract void RemoveNetworked(TCollectionItem item);
        protected abstract void RemoveAtNetworked(TKey key);
        protected abstract void SetValueNetworked(TKey key, TValue value);
        protected abstract void ResetNetworked();

#region  Local
        public int Count(){return LocalValue.Count();}
        public TValue GetValue(TKey key){return LocalValue[key];}
        public bool TryGetValue(TKey key, out TValue value){return LocalValue.TryGetValue(key, out value);}

        public void AddWithoutNotify(TKey key, TValue value)
        {
            if(checkForOwnership & !IsOwner)
                return;
            
            LocalValue.Add(key, value);
            AddNetworked(key, value);
        }

        public void AddWithoutNotify(TCollectionItem collectionItem)
        {
            if(checkForOwnership & !IsOwner)
                return;
            
            LocalValue.Add(collectionItem);
            AddNetworked(collectionItem);
        }

        public void RemoveAtWithoutNotify(TKey key)
        {
            if(checkForOwnership & !IsOwner)
                return;
            
            LocalValue.RemoveAt(key);
            RemoveAtNetworked(key);
        }

        public void RemoveWithoutNotify(TCollectionItem collectionItem)
        {
            if(checkForOwnership & !IsOwner)
                return;
            
            LocalValue.RemoveAt(LocalValue.ConvertCollectionItemToKey(collectionItem));
            RemoveNetworked(collectionItem);
        }

        public void SetValueWithoutNotify(TKey key, TValue value)
        {
            if(checkForOwnership & !IsOwner)
                return;

            LocalValue[key] = value;
            SetValueNetworked(key, value);
        }

        public void ResetCollection()
        {
            if(checkForOwnership & !IsOwner)
                return;

            LocalValue.ResetCollection();
            ResetNetworked();
        }

        public TKey ConvertCollectionItemToKey(TCollectionItem collectionItem)
        {
            return LocalValue.ConvertCollectionItemToKey(collectionItem);
        }

        public TValue ConvertCollectionItemToValue(TCollectionItem collectionItem)
        {
            return LocalValue.ConvertCollectionItemToValue(collectionItem);
        }

        public void AddListener(Action<CollectionModificationType, TKey, TValue> action)
        {
            LocalValue.AddListener(action);
        }

        public void RemoveListener(Action<CollectionModificationType, TKey, TValue> action)
        {
            LocalValue.AddListener(action);
        }

        public void OnCollectionChange(CollectionModificationType modificationType, TKey key, TValue value)
        {
            
        }
        #endregion
    }
}
#endif
