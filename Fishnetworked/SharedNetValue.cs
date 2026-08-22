#if Fishy
using System;
using FishNet.Object;
#endif
using UnityEngine;

namespace SharedValues.Fishnetworked
{
    public abstract class SharedNetValue<T, R> : 
#if Fishy
    NetworkBehaviour
#else
    MonoBehaviour
#endif
    where R : SharedValueReference<T>
    {
#if UNITY_EDITOR
        [SerializeField] private string Note;
#endif
        [SerializeField] private bool setNetVarOnInit;

#if Fishy //other networking protocols here
        [SerializeField] private bool checkForOwnership;
#endif        

        [SerializeField] private R localValue;
        protected R LocalValue => localValue;

        public T Value {get { return localValue.Value; } set { localValue.Value = value; }}

#if Fishy
        public override void OnStartNetwork()
        {
            base.OnStartNetwork();

            if(setNetVarOnInit)
            {
                SetValue(localValue.Value);
            }
        }
#endif

        public void SetValue(T value)
        {
            if(checkForOwnership & !IsOwner) return;

            SetNetworkValue(value);
        }

        protected abstract void SetNetworkValue(T value);
#if Fishy
        protected abstract void SetLocalValue(T previousValue, T nextValue, bool asServer);
#endif
        protected void SetLocalValue(T value)
        {
            #if UNITY_EDITOR
            Debug.Log($"the local value of {this.name} was set to {value}");
            #endif
            localValue.Value = value;
        }

        /// <summary>
        /// Will set the netvar the next time the network object is initialized
        /// </summary>
        /// <param name="value"></param>
        public void PrimeOnStartNetwork(T value)
        {
            localValue.Value = value;
            setNetVarOnInit = true;
        }

        public void AddLocalListener(Action<T> action)
        {
            localValue.AddListener(action);
        }

        public void RemoveLocalListener(Action<T> action)
        {
            localValue.RemoveListener(action);
        }
    }
}
