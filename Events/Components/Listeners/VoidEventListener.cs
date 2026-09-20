
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
    public class VoidEventListener : MonoBehaviour
    {
        private enum UnsubscribeTime
        {
            Disable,
            Destroy,
            Both,
        }

        [SerializeField] private UnsubscribeTime unsubscribeTime;
        [SerializeField] private SharedVoidEventReference _eventToListenTo;

        [SerializeField] UnityEvent onEventHeard;


        void OnEnable()
        {
            _eventToListenTo.RemoveListener(OnEventHeard);
            _eventToListenTo.AddListener(OnEventHeard);
        }

        private void OnEventHeard()
        {
            onEventHeard?.Invoke();
        }

        void OnDisable()
        {
            if(unsubscribeTime != UnsubscribeTime.Destroy)
                _eventToListenTo.RemoveListener(OnEventHeard);
        }

        void OnDestroy()
        {
            if(unsubscribeTime != UnsubscribeTime.Disable)
                _eventToListenTo.RemoveListener(OnEventHeard);
        }
    }
}
