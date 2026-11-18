
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
using System;
using UnityEngine;

namespace SharedValues.Upgradable
{
    [Serializable]
    public class UpgradableSharedVector2 : UpgradableSharedValue<Vector2, SharedVector2Reference, ValueModifierVector2, Vector2ValueModifications>
    {
        
    }

    [Serializable]
    public class Vector2ValueModifications : ValueModifications<Vector2, ValueModifierVector2>
    {
        protected override Vector2 ApplySpecificModification(Vector2 trackedValue, ValueModifierVector2 valueModifier, float modificationDomain)
        {
            return valueModifier.GetModifiedVector2(trackedValue, modificationDomain);
        }
    }

}
