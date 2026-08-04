using FishNet.Object;
using FishNet.Object.Synchronizing;

namespace SharedValues.Fishnetworked
{
    public class SharedNetInt : SharedNetValue<int, SharedIntReference>
    {
        private readonly SyncVar<int> networkValue = new SyncVar<int>();

        void Start()
        {
            SetValue(LocalValue.Value);
        }

        [ServerRpc]        
        public void SetValue(int value) 
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

        private void SetLocalValue(int prev, int next, bool asServer)
        {
            LocalValue.Value = next;
        }
    }
}