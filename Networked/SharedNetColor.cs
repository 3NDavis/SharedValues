#if Fishy
using FishNet.Object;
using FishNet.Object.Synchronizing;
#endif
using UnityEngine;

namespace SharedValues.Fishnetworked
{
    public class SharedNetColor : SharedNetValue<Color, SharedColorReference>
    {
        private readonly SyncVar<Color> networkValue = new SyncVar<Color>();


        [ServerRpc(RunLocally = true, RequireOwnership = false)] //require ownership is false since that check is already done in SetValue();
        protected override void SetNetworkValue(Color value)
        {
            //this causes the subscription in OnEnable to trigger
            networkValue.Value = value;
        }
        void OnEnable()
        {
            networkValue.OnChange += SetLocalValue;
        }

        void OnDisable()
        {
            networkValue.OnChange -= SetLocalValue;
        }

        protected override void SetLocalValue(Color prev, Color next, bool asServer)
        {
            SetLocalValue(next);
        }
    }
}