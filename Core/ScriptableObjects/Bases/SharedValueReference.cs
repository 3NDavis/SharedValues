using SharedValues.Attributes;
using System;
using UnityEngine;

namespace SharedValues
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

    public class SharedValueReference<T> : SharedValueReference
    {
        //[Visibility(nameof(_ReferenceType), ReferenceType.value)]
        [SerializeField] private T variableValue;
        private T VariableValue { get { return variableValue; } set { this.variableValue = value; onVariableValueChange?.Invoke(this.variableValue); } }
        public event Action<T> onVariableValueChange;
        
        //[Visibility(nameof(_ReferenceType), ReferenceType.value, true)]
        [SerializeField] private SharedValue<T> sharedReference;
        protected internal SharedValue<T> _SharedReference => sharedReference;

        //[Visibility(nameof(_ReferenceType), ReferenceType.groupedInstance)]
        [SerializeField] private ScriptableObjectInstancer instanceGroup;
        protected ScriptableObjectInstancer _instanceGroup => instanceGroup;

#if UNITY_EDITOR
        [Tooltip("<b>Playmode Only!</b> The value that the shared value reference is using, only updates when accessed.")]
        [SerializeField] private T actualValue;
#endif


        public void SetValueWithoutNotify(T value)
        {
            switch (_ReferenceType)
            {
                case ReferenceType.value:
                    this.variableValue = value;
                    break;
                case ReferenceType.global:
                    sharedReference.SetValueWithoutNotify(value);
                    break;
                case ReferenceType.instanced:
                    SharedValue<T> castSharedVal = (SharedValue<T>)instanceGroup.GetInstance(sharedReference);
                    castSharedVal.SetValueWithoutNotify(value);
                    break;

                default:
                    this.variableValue = value;
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
                        actualValue = VariableValue;
                        return (T)VariableValue;
                    case ReferenceType.global:
                        actualValue = sharedReference.Value;
                        return (T)sharedReference.Value;
                    case ReferenceType.instanced:
                        if (Application.isPlaying)
                        {
                            SharedValue<T> castSharedVal = (SharedValue<T>)instanceGroup.GetInstance(sharedReference);
                            actualValue = castSharedVal.Value;
                            return castSharedVal.Value;
                        }
                        actualValue = sharedReference.Value;
                        return sharedReference.Value;

                    default:
                        actualValue = variableValue;
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
                        sharedReference.Value = value;
                        break;
                    case ReferenceType.instanced:
                        SharedValue<T> castSharedVal = (SharedValue<T>)instanceGroup.GetInstance(sharedReference);
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
                    sharedReference.onValueChange += action;
                    break;
                case ReferenceType.instanced:
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                    //if(instanceGroup.sharedValueInstances == null)
                    //  Debug.Log($"The {instanceGroup.gameObject.name} does not have a shared value instance, this is likely because you are trying to subscribe in OnEnable.");
#endif
                    SharedValue<T> castSharedVal = (SharedValue<T>)instanceGroup.GetInstance(sharedReference);
                    castSharedVal.onValueChange += action;
                    break;

                default:
                    sharedReference.onValueChange += action;
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
                    sharedReference.onValueChange -= action;
                    break;
                case ReferenceType.instanced:
                    SharedValue<T> castSharedVal = (SharedValue<T>)instanceGroup.GetInstance(sharedReference);
                    castSharedVal.onValueChange -= action;
                    break;

                default:
                    sharedReference.onValueChange -= action;
                    break;
            }
        }
    }
}