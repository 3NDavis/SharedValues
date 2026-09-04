
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
        [SerializeField] private ScriptableObjectInstancer instanceGroup;
        protected ScriptableObjectInstancer InstanceGroup;
    }

    public abstract class SharedEventReference<T> : SharedEventReference
    {
        [SerializeField] private SharedEvent<T> evt;

        public bool HasEvent()
        {
            return evt != null;
        }

        public void AddListener(Action<T> action)
        {
            switch(_ReferenceType)
            {
                case ReferenceType.superGlobal:
                    evt.evt += action;
                    break;

                case ReferenceType.groupedInstance:
                    InstanceGroup.GetInstance(evt).evt += action;
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
                    InstanceGroup.GetInstance(evt).evt -= action;
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
                    InstanceGroup.GetInstance(evt).BroadcastEvent(value);
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

        public void AddListener(Action<T1, T2> action)
        {
            switch(_ReferenceType)
            {
                case ReferenceType.superGlobal:
                    evt.evt += action;
                    break;

                case ReferenceType.groupedInstance:
                    InstanceGroup.GetInstance(evt).evt += action;
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
                    InstanceGroup.GetInstance(evt).evt -= action;
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
                    InstanceGroup.GetInstance(evt).BroadcastEvent(value1, value2);
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

        public void AddListener(Action<T1, T2, T3> action)
        {
            switch(_ReferenceType)
            {
                case ReferenceType.superGlobal:
                    evt.evt += action;
                    break;

                case ReferenceType.groupedInstance:
                    InstanceGroup.GetInstance(evt).evt += action;
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
                    InstanceGroup.GetInstance(evt).evt -= action;
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
                    InstanceGroup.GetInstance(evt).BroadcastEvent(value1, value2, value3);
                    break;

                default:
                    evt.BroadcastEvent(value1, value2, value3);
                    break;
            }
        }
    }
}
