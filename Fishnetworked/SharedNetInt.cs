#if Fishy
using FishNet.Object;
using FishNet.Object.Synchronizing;
#endif

namespace SharedValues.Fishnetworked
{
    public class SharedNetInt : SharedNetValue<int, SharedIntReference>
    {
        private readonly SyncVar<int> networkValue = new SyncVar<int>();
        
#if Fishy
        [ServerRpc(RunLocally = true, RequireOwnership = false)] //require ownership is false since that check is already done in SetValue();
#endif
        protected override void SetNetworkValue(int value)
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

        protected override void SetLocalValue(int prev, int next, bool asServer)
        {
            SetValue(next);
        }
    }
}