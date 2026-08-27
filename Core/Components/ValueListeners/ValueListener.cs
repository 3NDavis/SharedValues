using UnityEngine;
using UnityEngine.Events;

namespace SharedValues
{
    public class ValueListener<T, R> : MonoBehaviour
    where R : SharedValueReference<T>
    {
        [SerializeField] private R value;
        [SerializeField] private UnityEvent<T> onValueChanged;
        [SerializeField] private bool broadcastOnEnable;

        void OnEnable()
        {
            value.AddListener(BroadcastEvent);
            if(broadcastOnEnable)
                BroadcastEvent(value.Value);
        }

        void OnDisable()
        {
            value.RemoveListener(BroadcastEvent);
        }

        protected virtual void BroadcastEvent(T newValue)
        {
            onValueChanged?.Invoke(newValue);
        }
    }
}