using UnityEngine;

namespace SharedValues
{
    public class SharedComponent<T> : SharedValue<T>
        where T : Component
    {

    }
}