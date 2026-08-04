using FishNet.Object;
using FishNet.Object.Synchronizing;

namespace SharedValues.Fishnetworked
{
    public class SharedNetFloat : SharedNetValue<float, SharedFloatReference>
    {
        private readonly SyncVar<float> networkValue = new SyncVar<float>();

        void Start()
        {
            SetValue(LocalValue.Value);
        }

        [ServerRpc]        
        public void SetValue(float value) 
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

        private void SetLocalValue(float prev, float next, bool asServer)
        {
            LocalValue.Value = next;
        }
    }
}