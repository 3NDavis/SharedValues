
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
    [CreateAssetMenu(menuName = "Shared Values/Events/Void", fileName = "SharedEvt_Void_Name")]
    public sealed class SharedVoidEvent : SharedSOBase
    {
        protected override string GetTextureName()
        {
            return "Void";
        }

        public Action evt;
        public void BroadcastEvent()
        {
            evt?.Invoke();
        }

        public void AddListener(Action action)
        {
            evt += action;
        }

        public void RemoveListener(Action action)
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
                    evt -= (Action)invocation;
                }
            }
        }
    }

    [Serializable]
    public sealed class SharedVoidEventReference : SharedEventReference
    {
        [SerializeField] SharedVoidEvent evt;

        public void AddListener(Action action)
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

        public void RemoveListener(Action action)
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

        public void BroadcastEvent()
        {
            switch (_ReferenceType)
            {
                case ReferenceType.superGlobal:
                    evt.BroadcastEvent();
                    break;
                case ReferenceType.groupedInstance:
                    InstanceGroup.GetInstance(evt).BroadcastEvent();
                    break;

                default:
                    evt.BroadcastEvent();
                    break;
            }
        }
    }
}
