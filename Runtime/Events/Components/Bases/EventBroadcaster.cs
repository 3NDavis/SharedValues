
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
   
   
   
using SharedValues.Core;
using UnityEngine;

namespace SharedValues.Events
{
    public abstract class EventBroadcaster<T, R, V> : MonoBehaviour
    where R : SharedEventReference<T> where V : SharedValueReference<T>
    {
        [SerializeField] private R eventToBroadcast;
        [SerializeField] private V valueToBroadcast;

#if UNITY_EDITOR
        private T lastValueBroadcasted;
        void Update()
        {
            var a = valueToBroadcast.Value;
        }
#endif

        public void Broadcast()
        {
            BroadcastValue(valueToBroadcast.Value);
        }

        public void BroadcastValue(T value)
        {
            eventToBroadcast.BroadcastEvent(value);
            #if UNITY_EDITOR
            lastValueBroadcasted = value;
            #endif
        }
    }
}