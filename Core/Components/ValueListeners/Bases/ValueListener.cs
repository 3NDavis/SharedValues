
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

namespace SharedValues
{
    public class ValueListener<T, R> : MonoBehaviour
    where R : SharedValueReference<T>
    {
        [SerializeField] private R value;
        [SerializeField] private UnityEvent<T> onValueChanged;
        [SerializeField] private bool broadcastOnEnable;

        void OnEnable()
        {
            value.AddListener(BroadcastEvent);
            if(broadcastOnEnable)
                BroadcastEvent(value.Value);
        }

        void OnDisable()
        {
            value.RemoveListener(BroadcastEvent);
        }

        protected virtual void BroadcastEvent(T newValue)
        {
            onValueChanged?.Invoke(newValue);
        }
    }
}
