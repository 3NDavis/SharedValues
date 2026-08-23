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