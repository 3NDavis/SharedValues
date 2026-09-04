
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
   
   
   
#if Fishy
using FishNet.Object;
using FishNet.Object.Synchronizing;
#endif
using UnityEngine;

namespace SharedValues.Networked
{
    public class SharedNetBool : SharedNetValue<bool, SharedBoolReference>
    {
        private readonly SyncVar<bool> networkValue = new SyncVar<bool>();

        [ServerRpc(RunLocally = true, RequireOwnership = false)] //require ownership is false since that check is already done in SetValue();
        protected override void SetNetworkValue(bool value)
        {
            //this causes the subscription in OnEnable to trigger
            networkValue.Value = value;
            #if UNITY_EDITOR
            Debug.Log($"the sync value of {this.name} was set to {value}");
            #endif
        }

        void OnEnable()
        {
            networkValue.OnChange += SetLocalValue;
        }

        void OnDisable()
        {
            networkValue.OnChange -= SetLocalValue;
        }

        protected override void SetLocalValue(bool prev, bool next, bool asServer)
        {
            SetLocalValue(next);
        }
    }
}
