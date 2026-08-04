using FishNet.Object;
using FishNet.Object.Synchronizing;

namespace SharedValues.Fishnetworked
{
    public class SharedNetBool : SharedNetValue<bool, SharedBoolReference>
    {
        private readonly SyncVar<bool> networkValue = new SyncVar<bool>();

        void Start()
        {
            SetValue(LocalValue.Value);
        }

        [ServerRpc]        
        public void SetValue(bool value) 
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

        private void SetLocalValue(bool prev, bool next, bool asServer)
        {
            LocalValue.Value = next;
        }
    }
}