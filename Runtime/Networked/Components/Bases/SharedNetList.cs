
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
using SharedValues.Core.Enumerators;
using System.Collections.Generic;

namespace SharedValues.Networked
{
    public abstract class SharedNetList<TValue, TReference> : SharedNetCollection<List<TValue>, TValue, int, TValue, TReference>
    where TReference: SharedListReference<TValue> 
    {
        public sealed override TValue GetValue(int key)
        {
            return Value[key];
        }

        public sealed override bool TryGetValue(int key, out TValue value)
        {
            if(Value.Count >= key)
            {
                value = default;
                return false;
            }

            value = Value[key];
            return true;
        }

        protected sealed override void AddNetworked(int key, TValue item)
        {
            AddNetworked(item);
        }

        protected void SetLocalValue(SyncListOperation op, int key, TValue oldValue, TValue newValue, bool asServer)
        {
            switch (op)
            {
                case SyncListOperation.Add:
                    Add(key, newValue);
                    break;
                case SyncListOperation.RemoveAt:
                    RemoveAt(key);
                    break;
                case SyncListOperation.Set:
                    SetValue(key, newValue);
                    break;
                case SyncListOperation.Clear:
                    LocalValue.ResetCollection();
                    break;
            }
        }
    }
}
#endif
