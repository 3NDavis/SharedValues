#if Fishy
using FishNet.Object;
using FishNet.Object.Synchronizing;
#endif

namespace SharedValues.Networked
{
    public class SharedNetFloat : SharedNetValue<float, SharedFloatReference>
    {
        private readonly SyncVar<float> networkValue = new SyncVar<float>();

        [ServerRpc(RunLocally = true, RequireOwnership = false)] //require ownership is false since that check is already done in SetValue();
        protected override void SetNetworkValue(float value)
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

        protected override void SetLocalValue(float prev, float next, bool asServer)
        {
            SetLocalValue(next);
        }
    }
}