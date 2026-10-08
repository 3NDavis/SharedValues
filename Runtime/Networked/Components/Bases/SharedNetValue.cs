
   //Copyright 2026 Ethan Davis

   //Licensed under the Apache License, Version 2.0 (the "License");
   //you may not use this file except in compliance with the License.
   //You may obtain a copy of the License at
   //  http://www.apache.org/licenses/LICENSE-2.0

   //Unless required by applicable law or agreed to in writing, software
   //distributed under the License is distributed on an "AS IS" BASIS,
   //WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   //See the License for the specific language governing permissions and
   //limitations under the License.
   
   
   
#if FISHNETWORKED
using FishNet.Object;
using SharedValues.Core;
using System;
using UnityEngine;

namespace SharedValues.Networked
{
    public abstract class SharedNetValue<TValue, TReference> : NetworkBehaviour, IValueSetter<TValue>, IValueEventHandler<TValue>
    where TReference : SharedValueReference<TValue>
    {
#if UNITY_EDITOR
        [SerializeField] protected string Note;
        [SerializeField] private bool debugLog;
#endif
        [field: SerializeField] protected bool checkForOwnership {get; private set;}

        [field: SerializeField] protected LocalInteractionMethod localInteractionMethod {get; private set;}
        protected enum LocalInteractionMethod
        {
            listen,
            broadcast,
        }

        [SerializeField] private TReference localValue;
        protected TReference LocalValue;
        
        public TValue Value {get { return localValue.Value; } set { SetValue(value); }}


        protected virtual void OnEnable()
        {
#if UNITY_EDITOR
            localValue.AddListener(UpdateNetValue);
#else
            if (listenToLocalValue)
            {
                localValue.AddListener(UpdateNetValue);
            }
#endif
        }
        protected virtual void OnDisable()
        {
            localValue.RemoveListener(UpdateNetValue);
        }

        private void UpdateNetValue(TValue value)
        {
#if UNITY_EDITOR
            if(localInteractionMethod != LocalInteractionMethod.listen) 
                return;
#endif
            if(value.Equals(localValue.Value)) 
                return;

            if(checkForOwnership & !IsOwner) 
                return;
                

            SetValue(value);
        }

        [Server(Logging = FishNet.Managing.Logging.LoggingType.Off)]
        protected void SetValue(TValue value)
        {
            if(checkForOwnership & !IsOwner) return;
            if(value.Equals(localValue.Value)) return;

#if UNITY_EDITOR
            if(debugLog)
                Debug.Log($"Trying to set Netvalue {Note} to {value}");
#endif

            SetLocalValue(value);
            SetNetworkValue(value);
        }

        // This attribute needs to be above SetNetworkValue(T) after it has been spcified
        
        // require ownership is false since that check is already done in SetValue();
        // [ServerRpc(RequireOwnership = false, RunLocally = false)] 
        protected abstract void SetNetworkValue(TValue value);
        public void SetLocalValue(TValue value)
        {
            #if UNITY_EDITOR
            if(debugLog)
                Debug.Log($"the local value of {this.name}'s {Note} was set to {value}");
            #endif
            localValue.Value = value;
        }

        protected void SetLocalValue(TValue prev, TValue next, bool asServer)
        {
            SetLocalValue(next);
        }

        /// <summary>
        /// Add listener to changes in the local value
        /// </summary>
        /// <param name="action"></param>
        public void AddListener(Action<TValue> action)
        {
            localValue.AddListener(action);
        }

        /// <summary>
        /// Removes listener to changes in the local value
        /// </summary>
        /// <param name="action"></param>
        public void RemoveListener(Action<TValue> action)
        {
            localValue.RemoveListener(action);
        }

#if UNITY_EDITOR
        //update inspector
        void Update()
        {
            var value = localValue.Value;
        }
#endif
    }
}
#endif
