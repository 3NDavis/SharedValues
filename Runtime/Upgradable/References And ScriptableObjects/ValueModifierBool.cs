
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

namespace SharedValues.Upgradable
{
    [CreateAssetMenu(menuName = "Shared Values/Upgradable/Value Modifer/Bool", fileName = "SharedValMod_Bool_Name")]
    public class ValueModifierBool : ValueModifierFloat
    {
        [SerializeField] private float trueDomain;
        [SerializeField] private float falseDomain;
        
        public override float GetModifiedFloat(float baseValue, float modificationDomain)
        {
            float acutalDomain = trueDomain;
            if(modificationDomain <= 0)
            {
                acutalDomain = falseDomain;
            }
            return base.GetModifiedFloat(baseValue, acutalDomain);
        }
    }
}
