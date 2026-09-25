
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
    public class BoolValueListener : ValueListenerToUnityEvent<bool, SharedBoolReference>
    {
        [Tooltip("Will broadcast only the oppropriate message for the condition. Otherwise, both are brodcasted")]
        [SerializeField] private bool broadcastConditionally;
        [SerializeField] private UnityEvent<bool> onValueChangeInverted;

        protected override void OnValueChanged(bool newValue)
        {
            if (broadcastConditionally)
            {
                if(newValue)
                    base.OnValueChanged(newValue);
                else
                    onValueChangeInverted?.Invoke(true);
            }
            else
            {
                base.OnValueChanged(newValue);
                onValueChangeInverted?.Invoke(!newValue);
            }
        }
        
    }
}
