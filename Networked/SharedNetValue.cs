
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
using FishNet.Object;
using UnityEngine;

namespace SharedValues.Networked
{
    public abstract class SharedNetValue<T, R> : NetworkBehaviour, IValueSetter<T>, IValueEventHandler<T>
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
            Debug.Log($"the local value of {this.name}'s {Note} was set to {value}");
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

#if UNITY_EDITOR
        void Update()
        {
            var value = localValue.Value;
        }
#endif
    }
}

