using System;
using UnityEngine;


namespace SharedValues.Events
{
    [CreateAssetMenu(menuName = "Shared Values/Events/Void", fileName = "SharedEvt_Void_Name")]
    public sealed class SharedVoidEvent : ScriptableObject
    {
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

        [Visibility(nameof(_ReferenceType), ReferenceType.groupedInstance)]
        [SerializeField] private ScriptableObjectInstancer instanceGroup;

        public void AddListener(Action action)
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

        public void RemvoeListener(Action action)
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

        public void BroadcastEvent()
        {
            switch (_ReferenceType)
            {
                case ReferenceType.superGlobal:
                    evt.BroadcastEvent();
                    break;
                case ReferenceType.groupedInstance:
                    instanceGroup.GetInstance(evt).BroadcastEvent();
                    break;

                default:
                    evt.BroadcastEvent();
                    break;
            }
        }
    }
}