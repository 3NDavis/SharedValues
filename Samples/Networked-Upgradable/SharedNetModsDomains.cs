
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



#if FISHNET
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using SharedValues.Networked;
using SharedValues.Upgradable;
using UnityEngine;

namespace SharedValues.Samples
{
    public class SharedNetModsDomains : SharedNetDictionary<string, float, SharedModsDomainsReference>
    {
        private readonly SyncDictionary<string, float> networkValue 
            = new SyncDictionary<string, float>(new SyncTypeSettings(WritePermission.ClientUnsynchronized, ReadPermission.ExcludeOwner));

        private void OnEnable()
        {
            networkValue.OnChange += SetLocalValue;
        }

        private void OnDisable()
        {
            networkValue.OnChange -= SetLocalValue;
        }
        
        //require ownership is false since that check is already done in SetValue();
        [ServerRpc(RequireOwnership = false, RunLocally = false)] 
        protected override void AddNetworked(string key, float value)
        {
            networkValue.Add(key, value);
        }

        //require ownership is false since that check is already done in SetValue();
        [ServerRpc(RequireOwnership = false, RunLocally = false)] 
        protected override void RemoveAtNetworked(string key)
        {
            networkValue.Remove(key);
        }

        //require ownership is false since that check is already done in SetValue();
        [ServerRpc(RequireOwnership = false, RunLocally = false)] 
        protected override void SetValueNetworked(string key, float value)
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
        protected override void SetNetworkValue(Dictionary<string, float> collection)
        {
            networkValue.Collection = collection;
        }

#if UNITY_EDITOR
        [SerializeField] private List<StringFloatPair> netMods;
        [SerializeField] private List<StringFloatPair> localMods;

        [System.Serializable]
        private class StringFloatPair
        {
            public string key;
            public float domain;
            public StringFloatPair(string key, float domain){this.key = key; this.domain = domain;}
        }
        void Update()
        {
            netMods.Clear();
            localMods.Clear();
            foreach(var key in networkValue.Keys)
            {
                netMods.Add(new StringFloatPair(key, networkValue[key]));
            }
            foreach(var key in LocalValue.Value.Keys)
            {
                localMods.Add(new StringFloatPair(key, LocalValue[key]));
            }
        }
#endif
    }
}
#endif