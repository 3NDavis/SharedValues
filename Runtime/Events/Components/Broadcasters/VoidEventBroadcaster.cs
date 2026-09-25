using UnityEngine;

namespace SharedValues.Events
{
    public class VoidEventReference : MonoBehaviour
    {
        [SerializeField] private SharedVoidEventReference eventToBroadcast;

        public void Broadcast()
        {
            eventToBroadcast.BroadcastEvent();
        }

    }
}