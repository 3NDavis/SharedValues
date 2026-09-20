
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

namespace SharedValues.Events
{
    public class ValueEventListener<T, R> : MonoBehaviour
    where R : SharedEventReference<T>
    {
        private enum UnsubscribeTime
        {
            Disable,
            Destroy,
            Both,
        }

        [SerializeField] private UnsubscribeTime unsubscribeTime;

        [SerializeField] private R eventToListenTo;

        [SerializeField] UnityEvent<T> onEventHeard;


        void OnEnable()
        {
            eventToListenTo.RemoveListener(OnEventHeard);
            eventToListenTo.AddListener(OnEventHeard);
        }

        protected virtual void OnEventHeard(T value)
        {
            onEventHeard?.Invoke(value);
        }

        void OnDisable()
        {
            if(unsubscribeTime != UnsubscribeTime.Destroy)
                eventToListenTo.RemoveListener(OnEventHeard);
        }

        void OnDestroy()
        {
            if(unsubscribeTime != UnsubscribeTime.Disable)
                eventToListenTo.RemoveListener(OnEventHeard);
        }
    }
}
