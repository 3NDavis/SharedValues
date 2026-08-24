using System;
using FishNet.Object;
using UnityEngine;

namespace SharedValues.Networked
{
    public abstract class SharedNetValue<T, R> : NetworkBehaviour
    where R : SharedValueReference<T>
    {
#if UNITY_EDITOR
        [SerializeField] private string Note;
#endif
        [SerializeField] private bool setNetVarOnInit;

        [SerializeField] private bool checkForOwnership;

        [SerializeField] private R localValue;
        public T Value {get { return localValue.Value; } set { SetValue(value); }}

        public override void OnStartNetwork()
        {
            base.OnStartNetwork();

            if(setNetVarOnInit)
            {
                SetValue(localValue.Value);
            }
        }

        protected void SetValue(T value)
        {
            if(checkForOwnership & !IsOwner) return;

            SetNetworkValue(value);
        }

        protected abstract void SetNetworkValue(T value);
        protected abstract void SetLocalValue(T previousValue, T nextValue, bool asServer);
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

        /// <summary>
        /// Add listener to changes in the local value
        /// </summary>
        /// <param name="action"></param>
        public void AddListener(Action<T> action)
        {
            localValue.AddListener(action);
        }

        /// <summary>
        /// Removes listener to changes in the local value
        /// </summary>
        /// <param name="action"></param>
        public void RemoveListener(Action<T> action)
        {
            localValue.RemoveListener(action);
        }
    }
}
