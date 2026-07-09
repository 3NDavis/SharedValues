using UnityEngine;
using SharedValues.Attributes;
using System;


namespace SharedValues.Events
{
    public abstract class SharedEventReference
    {
        protected enum ReferenceType
        {
            superGlobal,
            groupedInstance,
        }

        [SerializeField] private ReferenceType referenceType;
        protected ReferenceType _ReferenceType => referenceType;
    }

    public abstract class SharedEventReference<T> : SharedEventReference
    {
        [SerializeField] private SharedEvent<T> evt;

        [Visibility(nameof(_ReferenceType), ReferenceType.groupedInstance)]
        [SerializeField] private ScriptableObjectInstancer instanceGroup;

        public void AddListener(Action<T> action)
        {
            switch(_ReferenceType)
            {
                case ReferenceType.superGlobal:
                    evt.evt += action;
                    break;

                case ReferenceType.groupedInstance:
                    instanceGroup.GetInstance(evt).evt += action;
                    break;
            }
        }

        public void RemoveListener(Action<T> action)
        {
            switch(_ReferenceType)
            {
                case ReferenceType.superGlobal:
                    evt.evt -= action;
                    break;

                case ReferenceType.groupedInstance:
                    instanceGroup.GetInstance(evt).evt -= action;
                    break;
            }
        }

        public void BroadcastEvent(T value)
        {
            switch (_ReferenceType)
            {
                case ReferenceType.superGlobal:
                    evt.BroadcastEvent(value);
                    break;
                case ReferenceType.groupedInstance:
                    instanceGroup.GetInstance(evt).BroadcastEvent(value);
                    break;

                default:
                    evt.BroadcastEvent(value);
                    break;
            }
        }
    }

    public abstract class SharedEventReference<T1, T2> : SharedEventReference
    {
        [SerializeField] private SharedEvent<T1, T2> evt;

        [Visibility(nameof(_ReferenceType), ReferenceType.groupedInstance)]
        [SerializeField] private ScriptableObjectInstancer instanceGroup;

        public void AddListener(Action<T1, T2> action)
        {
            switch(_ReferenceType)
            {
                case ReferenceType.superGlobal:
                    evt.evt += action;
                    break;

                case ReferenceType.groupedInstance:
                    instanceGroup.GetInstance(evt).evt += action;
                    break;
            }
        }

        public void RemvoeListener(Action<T1, T2> action)
        {
            switch(_ReferenceType)
            {
                case ReferenceType.superGlobal:
                    evt.evt -= action;
                    break;

                case ReferenceType.groupedInstance:
                    instanceGroup.GetInstance(evt).evt -= action;
                    break;
            }
        }

        public void BroadcastEvent(T1 value1, T2 value2)
        {
            switch (_ReferenceType)
            {
                case ReferenceType.superGlobal:
                    evt.BroadcastEvent(value1, value2);
                    break;
                case ReferenceType.groupedInstance:
                    instanceGroup.GetInstance(evt).BroadcastEvent(value1, value2);
                    break;

                default:
                    evt.BroadcastEvent(value1, value2);
                    break;
            }
        }
    }

    public abstract class SharedEventReference<T1, T2, T3> : SharedEventReference
    {
        [SerializeField] private SharedEvent<T1, T2, T3> evt;

        [Visibility(nameof(_ReferenceType), ReferenceType.groupedInstance)]
        [SerializeField] private ScriptableObjectInstancer instanceGroup;

        public void AddListener(Action<T1, T2, T3> action)
        {
            switch(_ReferenceType)
            {
                case ReferenceType.superGlobal:
                    evt.evt += action;
                    break;

                case ReferenceType.groupedInstance:
                    instanceGroup.GetInstance(evt).evt += action;
                    break;
            }
        }

        public void RemvoeListener(Action<T1, T2, T3> action)
        {
            switch(_ReferenceType)
            {
                case ReferenceType.superGlobal:
                    evt.evt -= action;
                    break;

                case ReferenceType.groupedInstance:
                    instanceGroup.GetInstance(evt).evt -= action;
                    break;
            }
        }

        public void BroadcastEvent(T1 value1, T2 value2, T3 value3)
        {
            switch (_ReferenceType)
            {
                case ReferenceType.superGlobal:
                    evt.BroadcastEvent(value1, value2, value3);
                    break;
                case ReferenceType.groupedInstance:
                    instanceGroup.GetInstance(evt).BroadcastEvent(value1, value2, value3);
                    break;

                default:
                    evt.BroadcastEvent(value1, value2, value3);
                    break;
            }
        }
    }
}