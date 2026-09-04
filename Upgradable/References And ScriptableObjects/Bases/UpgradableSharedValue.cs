
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
   
   
   
using System.Collections.Generic;
using UnityEngine;

namespace SharedValues.Upgradable
{
    public abstract class UpgradableSharedValue{}

    /// <summary>
    /// A shared value that has modifiers applied to it
    /// </summary>
    public abstract class UpgradableSharedValue<T,M,V> : UpgradableSharedValue
    where T : struct where M : ValueModifier where V : ValueModifications<T,M>
    {
        [SerializeField] private T baseValue;
        [Tooltip("<b>Playmode Only!</b> The value that the shared value reference is using, only updates when accessed.")]
        [SerializeField] private T postModificationValue;

        [SerializeField] V modifiers;

        public T GetValue(Dictionary<ValueModifier, float> modificationsDomains)
        {
            postModificationValue = modifiers.ApplyModifications(baseValue, modificationsDomains);
            return postModificationValue;
        }
    }
}
