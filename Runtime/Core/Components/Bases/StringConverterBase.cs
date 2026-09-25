
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

namespace SharedValues.Core
{
    /// <summary>
    /// The base class for components that listen to valueToListenTo and update a string SharedValueReference
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="SVR"></typeparam>
    public abstract class StringConverterBase<T, SVR> : ValueListener<T, SVR>
    where SVR : SharedValueReference<T>
    {
        [SerializeField] private SVR value;
        [SerializeField] private SharedStringReference stringValue;

        protected override void OnValueChanged(T value)
        {
            ConvertToString(value);
        }

        /// <summary>
        /// converts <paramref name="value"/> to a string and sets the stringValue
        /// </summary>
        /// <param name="value">the value to convert to a string</param>
        private void ConvertToString(T value)
        {
            stringValue.Value = value.ToString();
        }

#if UNITY_EDITOR
        //this updates the inspector only value for the shared value
        //since its inspector only, it shouldn't be compiled in builds
        protected override void Update()
        {
            base.Update();
            var s = stringValue.Value;
        }
#endif
    }
}