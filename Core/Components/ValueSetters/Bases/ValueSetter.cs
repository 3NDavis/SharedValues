
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

namespace SharedValues
{
    [System.Serializable]
    public class SetValuePair<T>
    {
        public SharedValue<T> valueReference;
        public T valueToSetTo;
        
    }
    public class ValueSetter<T> : MonoBehaviour
    {
        [SerializeField] List<SharedValueReference<T>> valueReferences = new();
        Dictionary<SharedValue<T>, SharedValueReference<T>> valueReferencePairs;

        void Awake()
        {
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            valueReferencePairs = new();
            for (int i = 0; i < valueReferences.Count; i++)
            {
                valueReferencePairs.TryAdd(valueReferences[i]._SharedReference, valueReferences[i]);
            }
        }

        public void SetValueReferences(params SetValuePair<T>[] valuePairs)
        {
            if(valueReferencePairs == null)
                InitializeDictionary();

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
