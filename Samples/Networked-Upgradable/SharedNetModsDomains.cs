
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
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using SharedValues.Networked;
using SharedValues.Upgradable;

namespace SharedValues.Samples
{
    public class SharedNetModsDomains : SharedNetDictionary<ValueModifierFloat, float, SharedModsDomainsReference>
    {
        private readonly SyncDictionary<ValueModifierFloat, float> networkValue 
            = new SyncDictionary<ValueModifierFloat, float>(new SyncTypeSettings(WritePermission.ClientUnsynchronized, ReadPermission.ExcludeOwner));

        protected override void OnEnable()
        {
            base.OnEnable();
            networkValue.OnChange += SetLocalValue;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            networkValue.OnChange -= SetLocalValue;
        }
        
        //require ownership is false since that check is already done in SetValue();
        [ServerRpc(RequireOwnership = false, RunLocally = false)] 
        protected override void AddNetworked(ValueModifierFloat key, float value)
        {
            networkValue.Add(key, value);
        }

        //require ownership is false since that check is already done in SetValue();
        [ServerRpc(RequireOwnership = false, RunLocally = false)] 
        protected override void RemoveAtNetworked(ValueModifierFloat key)
        {
            networkValue.Remove(key);
        }

        //require ownership is false since that check is already done in SetValue();
        [ServerRpc(RequireOwnership = false, RunLocally = false)] 
        protected override void SetValueNetworked(ValueModifierFloat key, float value)
        {
            networkValue[key] = value;
        }

        //require ownership is false since that check is already done in SetValue();
        [ServerRpc(RequireOwnership = false, RunLocally = false)] 
        protected override void ResetNetworked()
        {
            networkValue.Clear();
        }

        //require ownership is false since that check is already done in SetValue();
        [ServerRpc(RequireOwnership = false, RunLocally = false)] 
        protected override void SetNetworkValue(Dictionary<ValueModifierFloat, float> collection)
        {
            networkValue.Collection = collection;
        }
    }
}