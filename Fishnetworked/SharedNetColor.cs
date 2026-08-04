using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace SharedValues.Fishnetworked
{
    public class SharedNetColor : SharedNetValue<Color, SharedColorReference>
    {
        private readonly SyncVar<Color> networkValue = new SyncVar<Color>();

        void Start()
        {
            SetValue(LocalValue.Value);
        }

        [ServerRpc]        
        public void SetValue(Color value) 
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

        private void SetLocalValue(Color prev, Color next, bool asServer)
        {
            LocalValue.Value = next;
        }
    }
}