
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
   
   
   
using System;
using UnityEngine;

namespace SharedValues.Core
{
    public abstract class SharedValueReference
    {
        protected enum ReferenceType
        {
            value,
            global,
            instanced,
        }

        [SerializeField] private ReferenceType referenceType;
        protected ReferenceType _ReferenceType => referenceType;
    }

    public class SharedValueReference<T> : SharedValueReference, IValueEventHandler<T>
    {
        [SerializeField] private T variableValue;
        private T VariableValue { get { return variableValue; } set { this.variableValue = value; onVariableValueChange?.Invoke(this.variableValue); } }
        public event Action<T> onVariableValueChange;
        
        [SerializeField] private SharedValue<T> sharedValue;
        protected internal SharedValue<T> SharedValue => sharedValue;

        [SerializeField] private ScriptableObjectInstancer instanceGroup;
        protected ScriptableObjectInstancer InstanceGroup => instanceGroup;

        /// <summary>
        /// Allows the shared value to be used in the place of a value.
        /// Ex sharedIntReference += 1 instead of sharedIntReference.Value += 1
        /// </summary>
        /// <param name="reference"></param>
		public static implicit operator T( SharedValueReference<T> reference )
		{
			return reference.Value;
		}

#if UNITY_EDITOR
        [Tooltip("<b>Playmode Only!</b> The value that the shared value reference is using, only updates when accessed.")]
        [SerializeField] private T actualValue;
#endif


        public void SetValueWithoutNotify(T value)
        {
            switch (_ReferenceType)
            {
                case ReferenceType.value:
                    this.VariableValue = value;
                    break;
                case ReferenceType.global:
                    sharedValue.SetValueWithoutNotify(value);
                    break;
                case ReferenceType.instanced:
                    SharedValue<T> castSharedVal = (SharedValue<T>)instanceGroup.GetInstance(sharedValue);
                    castSharedVal.SetValueWithoutNotify(value);
                    break;

                default:
                    this.VariableValue = value;
                    break;
            }
            SetActualValue();
        }

        private void SetActualValue()
        {
#if UNITY_EDITOR
            actualValue = Value;
#endif
        }

        /// <summary>
        /// Will set the value and broadcast a change if the value is different to the current one
        /// </summary>
        /// <param name="value"></param>
        public void SetValueWithBroadcastIfChange(T value)
        {
            if(Value.Equals(value))
                return;
            
            Value = value;
        }

        public T Value
        {
            get
            {
                switch (_ReferenceType)
                {
                    case ReferenceType.value:
                    #if UNITY_EDITOR
                        actualValue = VariableValue;
                    #endif
                        return (T)VariableValue;
                    case ReferenceType.global:
                    #if UNITY_EDITOR
                        actualValue = sharedValue.Value;
                    #endif
                        return (T)sharedValue.Value;
                    case ReferenceType.instanced:
                        if (Application.isPlaying)
                        {
                            SharedValue<T> castSharedVal = (SharedValue<T>)instanceGroup.GetInstance(sharedValue);
                    #if UNITY_EDITOR
                            actualValue = castSharedVal.Value;
                    #endif
                            return castSharedVal.Value;
                        }
                    #if UNITY_EDITOR
                        actualValue = sharedValue.Value;
                    #endif
                        return sharedValue.Value;

                    default:
                    #if UNITY_EDITOR
                        actualValue = variableValue;
                    #endif
                        return (T)VariableValue;
                }
            }
            set
            {
                switch (_ReferenceType)
                {
                    case ReferenceType.value:
                        VariableValue = value;
                        break;
                    case ReferenceType.global:
                        sharedValue.Value = value;
                        break;
                    case ReferenceType.instanced:
                        SharedValue<T> castSharedVal = (SharedValue<T>)instanceGroup.GetInstance(sharedValue);
                        castSharedVal.Value = value;
                        break;

                    default:
                        VariableValue = value;
                        break;
                }
                SetActualValue();
            }
        }

        public void AddListener(Action<T> action)
        {
            switch (_ReferenceType)
            {
                case ReferenceType.value:
                    onVariableValueChange += action;
                    break;

                case ReferenceType.global:
                    sharedValue.onValueChange += action;
                    break;
                case ReferenceType.instanced:
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                    //if(instanceGroup.sharedValueInstances == null)
                    //  Debug.Log($"The {instanceGroup.gameObject.name} does not have a shared value instance, this is likely because you are trying to subscribe in OnEnable.");
#endif
                    SharedValue<T> castSharedVal = (SharedValue<T>)instanceGroup.GetInstance(sharedValue);
                    castSharedVal.onValueChange += action;
                    break;

                default:
                    sharedValue.onValueChange += action;
                    break;
            }
        }

        public void RemoveListener(Action<T> action)
        {
            switch (_ReferenceType)
            {
                case ReferenceType.value:
                    onVariableValueChange -= action;
                    break;
                case ReferenceType.global:
                    sharedValue.onValueChange -= action;
                    break;
                case ReferenceType.instanced:
                    SharedValue<T> castSharedVal = (SharedValue<T>)instanceGroup.GetInstance(sharedValue);
                    castSharedVal.onValueChange -= action;
                    break;

                default:
                    sharedValue.onValueChange -= action;
                    break;
            }
        }
    }
}
