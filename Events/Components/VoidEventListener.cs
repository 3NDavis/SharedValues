using UnityEngine;
using UnityEngine.Events;

namespace SharedValues.Events
{
    public class VoidEventListener : MonoBehaviour
    {
        [SerializeField] private SharedVoidEvent eventToListenTo;

        [SerializeField] UnityEvent onEventHeard;


        void OnEnable()
        {
            eventToListenTo.AddListener(PlayEffect);
        }

        private void PlayEffect()
        {
            onEventHeard?.Invoke();
        }

        void OnDisable()
        {
            eventToListenTo.RemoveListener(PlayEffect);
        }
    }
}