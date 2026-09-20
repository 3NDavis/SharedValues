
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
   
   
   
using System;
using System.Collections;
using FishNet.Object;
using UnityEngine;

namespace SharedValues.Networked
{
    public abstract class SharedNetValue<T, R> : NetworkBehaviour, IValueSetter<T>, IValueEventHandler<T>
    where R : SharedValueReference<T>
    {
#if UNITY_EDITOR
        [SerializeField] protected string Note;
        [SerializeField] private bool debugLog;
#endif
        [SerializeField] private bool checkForOwnership;

        [SerializeField] private bool setValueOnStart;
        [SerializeField] private T startingValue;
        [SerializeField] private R localValue;
        
        public T Value {get { return localValue.Value; } set { SetValue(value); }}

        bool isInitialized;

        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            isInitialized = true;
        }

        [Server(Logging = FishNet.Managing.Logging.LoggingType.Off)]
        protected void SetValue(T value)
        {
            if(checkForOwnership & !IsOwner) return;

            if (!isInitialized)
            {
                StopAllCoroutines();
                StartCoroutine(WaitForInitialized(value));
            }

#if UNITY_EDITOR
            if(debugLog)
                Debug.Log($"Trying to set Netvalue {Note} to {value}");
#endif

            SetLocalValue(value);
            SetNetworkValue(value);
        }

        private IEnumerator WaitForInitialized(T value)
        {
            //wait 1 frame after initialized
            yield return new WaitUntil(() => isInitialized);
            yield return new();

            SetValue(value);
        }

        // This attribute needs to be above SetNetworkValue(T) after it has been spcified
        
        // require ownership is false since that check is already done in SetValue();
        // [ServerRpc(RequireOwnership = false, RunLocally = true)] 
        protected abstract void SetNetworkValue(T value);
        public void SetLocalValue(T value)
        {
            #if UNITY_EDITOR
            if(debugLog)
                Debug.Log($"the local value of {this.name}'s {Note} was set to {value}");
            #endif
            localValue.Value = value;
        }

        protected void SetLocalValue(T prev, T next, bool asServer)
        {
            SetLocalValue(next);
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

#if UNITY_EDITOR
        //update inspector
        void Update()
        {
            var value = localValue.Value;
        }
#endif
    }
}

