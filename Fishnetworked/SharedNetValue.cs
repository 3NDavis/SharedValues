using UnityEngine;
using FishNet.Object;

namespace SharedValues.Fishnetworked
{
    public class SharedNetValue<T, R> : NetworkBehaviour
    where R : SharedValueReference<T>
    {
        [SerializeField] private R localValue;
        protected R LocalValue => localValue;
    }
}
