
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
   
   
   
using UnityEngine;
using UnityEngine.Events;

namespace SharedValues.Core
{
    /// <summary>
    /// The base class for components that listen to a SharedValueReference
    /// </summary>
    /// <typeparam name="T">The type of SharedValue</typeparam>
    /// <typeparam name="SVR">The SharedValueReference of type <typeparamref name="T"/></typeparam>
    public abstract class ValueListener<T, SVR> : MonoBehaviour
    where SVR : SharedValueReference<T>
    {
        private enum UnsubscribeTime
        {
            Disable,
            Destroy,
            Both,
        }


        ///<summary>When should this component unsubscribe from the value</summary>
        [Tooltip("When should this component unsubscribe from the value")]
        [SerializeField] private UnsubscribeTime unsubscribeTime;

        ///<summary>The value to listen to</summary>
        [Tooltip("The value to listen to")]
        [SerializeField] protected SVR valueToListenTo;

        ///<summary>Should this componnent do its logic to broadcast onEnable</summary>
        [Tooltip("Should this componnent do its logic to broadcast onEnable")]
        [SerializeField] private bool doOnEnable;

        void OnEnable()
        {
            //removes listener in case this component is already subscribed
            //this can happen if the unsubscribe time is onDestroy, and the component is disabled and enabled.
            valueToListenTo.RemoveListener(OnValueChanged);

            //add the listener
            valueToListenTo.AddListener(OnValueChanged);

            //Do the logic onEnable if it should be done
            if(doOnEnable)
                OnValueChanged(valueToListenTo.Value);
        }

        void OnDisable()
        {
            //unsubscribe from the value if the UnsubscribeTime is Disable or Both
            if(unsubscribeTime != UnsubscribeTime.Destroy)
                valueToListenTo.RemoveListener(OnValueChanged);
        }

        void OnDestroy()
        {
            //unsubscribe from the value if the UnsubscribeTime is Destroy or Both
            if(unsubscribeTime != UnsubscribeTime.Disable)
                valueToListenTo.RemoveListener(OnValueChanged);
        }

        /// <summary>
        /// When the valueToListenTo broadcasts it's value has changed
        /// </summary>
        /// <param name="newValue">the valueToListenTo's new value</param>
        protected abstract void OnValueChanged(T newValue);

#if UNITY_EDITOR
        //this updates the inspector only value for the shared value
        //since its inspector only, it shouldn't be compiled in builds
        protected virtual void Update()
        {
            var a = valueToListenTo.Value;
        }
#endif
    }
}
