using UnityEngine;
using UnityEngine.Events;

namespace SharedValues
{
    public class ValueListener<T, R> : MonoBehaviour
    where R : SharedValueReference<T>
    {
        [SerializeField] private R value;
        [SerializeField] private UnityEvent<T> onValueChanged;

        void OnEnable()
        {
            value.AddListener(BroadcastEvent);
        }

        void OnDisable()
        {
            value.RemoveListener(BroadcastEvent);
        }

        private void BroadcastEvent(T newValue)
        {
            onValueChanged?.Invoke(newValue);
        }
    }
}