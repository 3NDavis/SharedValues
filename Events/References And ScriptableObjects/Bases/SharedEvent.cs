
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

namespace SharedValues.Events
{
    public abstract class SharedEvent<T> : SharedEventSOBase
    {
        public Action<T> evt;

        public void BroadcastEvent(T value)
        {
            evt?.Invoke(value);
        }

        public void BroadcastEvent(SharedValueReference<T> reference)
        {
            evt?.Invoke(reference.Value);
        }

        public void AddListener(Action<T> action)
        {
            evt += action;
        }

        public void RemoveListener(Action<T> action)
        {
            evt -= action;
        }

        void OnDestroy()
        {
            if (evt != null)
            {
                var invocationList = evt.GetInvocationList();
                foreach (var invocation in invocationList)
                {
                    evt -= (Action<T>)invocation;
                }
            }
        }
    }

    public abstract class SharedEvent<T1, T2> : ScriptableObject
    {
        public Action<T1, T2> evt;

        public void BroadcastEvent(T1 value1, T2 value2)
        {
            evt?.Invoke(value1, value2);
        }

        public void BroadcastEvent(SharedValueReference<T1> reference1, SharedValueReference<T2> reference2)
        {
            evt?.Invoke(reference1.Value, reference2.Value);
        }

        public void AddListener(Action<T1, T2> action)
        {
            evt += action;
        }

        public void RemoveListener(Action<T1, T2> action)
        {
            evt -= action;
        }

        void OnDestroy()
        {
            if (evt != null)
            {
                var invocationList = evt.GetInvocationList();
                foreach (var invocation in invocationList)
                {
                    evt -= (Action<T1, T2>)invocation;
                }
            }
        }
    }

    public abstract class SharedEvent<T1, T2, T3> : ScriptableObject
    {
        public Action<T1, T2, T3> evt;

        public void BroadcastEvent(T1 value1, T2 value2, T3 value3)
        {
            evt?.Invoke(value1, value2, value3);
        }

        public void BroadcastEvent(SharedValueReference<T1> reference1, SharedValueReference<T2> reference2, SharedValueReference<T3> reference3)
        {
            evt?.Invoke(reference1.Value, reference2.Value, reference3.Value);
        }

        public void AddListener(Action<T1, T2, T3> action)
        {
            evt += action;
        }

        public void RemoveListener(Action<T1, T2, T3> action)
        {
            evt -= action;
        }

        void OnDestroy()
        {
            if (evt != null)
            {
                var invocationList = evt.GetInvocationList();
                foreach (var invocation in invocationList)
                {
                    evt -= (Action<T1, T2, T3>)invocation;
                }
            }
        }
    }
}
