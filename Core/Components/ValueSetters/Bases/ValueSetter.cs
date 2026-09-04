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