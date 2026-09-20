using UnityEngine;

namespace SharedValues.Events
{
    public abstract class EventBroadcaster<T, R, V> : MonoBehaviour
    where R : SharedEventReference<T> where V : SharedValueReference<T>
    {
        [SerializeField] private R eventToBroadcast;
        [SerializeField] private V valueToBroadcast;

#if UNITY_EDITOR
        private T lastValueBroadcasted;
        void Update()
        {
            var a = valueToBroadcast.Value;
        }
#endif

        public void Broadcast()
        {
            BroadcastValue(valueToBroadcast.Value);
        }

        public void BroadcastValue(T value)
        {
            eventToBroadcast.BroadcastEvent(value);
            #if UNITY_EDITOR
            lastValueBroadcasted = value;
            #endif
        }
    }
}