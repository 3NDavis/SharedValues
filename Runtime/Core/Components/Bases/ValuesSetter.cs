
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

namespace SharedValues.Core
{
    [System.Serializable]
    public class SetValuePair<T, SV>
    where SV : SharedValue<T>
    {
        public SharedValue<T> valueReference;
        public T valueToSetTo;
        
    }

    public static class ValueSetter
    {
        public static void UseValueSetter<T, SV, SVR>(GameObject attatchment, SetValuePair<T, SV>[] valuePairs)
        where SVR : SharedValueReference<T> 
        where SV : SharedValue<T>
        {
            if (attatchment.TryGetComponent<ValuesSetter<T, SV, SVR>>(out var valueSetter))
            {
                valueSetter.SetValueReferences(valuePairs);
            }
        }
    }

    public class ValuesSetter<T, SV, SVR> : MonoBehaviour
    where SVR : SharedValueReference<T> 
    where SV : SharedValue<T>
    {
        [SerializeField] List<SVR> valueReferences;
        Dictionary<SharedValue<T>, SVR> valueReferencePairs;

        void Awake()
        {
            valueReferences = new();
            valueReferencePairs = new(); 

            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            for (int i = 0; i < valueReferences.Count; i++)
            {
                valueReferencePairs.TryAdd(valueReferences[i].SharedValue, valueReferences[i]);
            }
        }

        public void SetValueReferences(params SetValuePair<T, SV>[] valuePairs)
        {
            if(valuePairs == null)
                return;

            for (int i = 0; i < valuePairs.Length; i++)
            {
                if(valueReferencePairs.TryGetValue(valuePairs[i].valueReference, out var reference))
                {
                    reference.Value = valuePairs[i].valueToSetTo;
                }
            }
        }
    }
}
