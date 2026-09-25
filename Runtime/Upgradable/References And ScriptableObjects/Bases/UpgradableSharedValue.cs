
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
using System.Collections.Generic;
using UnityEngine;

namespace SharedValues.Upgradable
{
    public abstract class UpgradableSharedValue{}

    /// <summary>
    /// A shared value that has modifiers applied to it
    /// </summary>
    public abstract class UpgradableSharedValue<T,SVR, M,V> : UpgradableSharedValue
    where T : struct where SVR : SharedValueReference<T> where M : ValueModifierFloat where V : ValueModifications<T,M>
    {
        [SerializeField] private SVR baseValue;
        [Tooltip("<b>Playmode Only!</b> The value that the shared value reference is using, only updates when accessed.")]
        [SerializeField] private T postModificationValue;

        [SerializeField] V modifiers;

        public T GetValue(Dictionary<ValueModifierFloat, float> modificationsDomains)
        {
            postModificationValue = modifiers.ApplyModifications(baseValue.Value, modificationsDomains);
            return postModificationValue;
        }
    }
}
