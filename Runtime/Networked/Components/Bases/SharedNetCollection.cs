
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
using SharedValues.Core.Enumerators;
using System.Collections;

namespace SharedValues.Networked
{
    public abstract class SharedNetCollection<TCollection, TCollectionItem, TKey, TValue, TReference> : SharedNetValue<TCollection, TReference>, ISharedCollection<TCollectionItem, TKey, TValue>
    where TCollection : ICollection, IEnumerable
    where TReference : SharedCollectionReference<TCollection, TCollectionItem, TKey, TValue>
    {
#region Local/Network
        // indexer declaration
        public TValue this[TKey key]
        {
            get => GetValue(key);
            set => SetValue(key, value);
        }

        public void ResetCollection()
        {
            if(checkForOwnership & !IsOwner)
                return;

            LocalValue.ResetCollection();
            ResetNetworked();
        }
        public void Add(TCollectionItem item)
        {
            if(checkForOwnership & !IsOwner)
                return;
            
            LocalValue.Add(item);
            AddNetworked(item);
        }
        public void Add(TKey key, TValue value)
        {
            if(checkForOwnership & !IsOwner)
                return;
            
            LocalValue.Add(key, value);
            AddNetworked(key, value);
        }
        public void Remove(TCollectionItem item)
        {
            if(checkForOwnership & !IsOwner)
                return;
            
            LocalValue.Remove(item);
            Remove(item);
        }
        public void RemoveAt(TKey key)
        {
            if(checkForOwnership & !IsOwner)
                return;

            LocalValue.RemoveAt(key);
            RemoveAtNetworked(key);
        }
        public void SetValue(TKey key, TValue value)
        {
            if(checkForOwnership & !IsOwner)
                return;
            
            LocalValue.SetValue(key, value);
            SetValueNetworked(key, value);
        }
#endregion

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
        public abstract TValue GetValue(TKey key);
        public abstract bool TryGetValue(TKey key, out TValue value);
#endregion
    }
}
#endif
