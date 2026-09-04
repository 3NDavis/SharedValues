using UnityEngine;
using UnityEngine.Events;

namespace SharedValues.Events
{
    public class ValueEventListener<T, R> : MonoBehaviour
    where R : SharedEventReference<T>
    {
        [SerializeField] private R eventToListenTo;

        [SerializeField] UnityEvent<T> onEventHeard;


        void OnEnable()
        {
            eventToListenTo.AddListener(PlayEffect);
        }

        private void PlayEffect(T value)
        {
            onEventHeard?.Invoke(value);
        }

        void OnDisable()
        {
            eventToListenTo.RemoveListener(PlayEffect);
        }
    }
}