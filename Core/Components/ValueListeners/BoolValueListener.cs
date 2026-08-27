using UnityEngine;
using UnityEngine.Events;

namespace SharedValues
{
    public class BoolValueListener : ValueListener<bool, SharedBoolReference>
    {
        [SerializeField] private UnityEvent<bool> onValueChangedInverted;

        protected override void BroadcastEvent(bool newValue)
        {
            base.BroadcastEvent(newValue);
            onValueChangedInverted?.Invoke(!newValue);
        }
        
    }
}