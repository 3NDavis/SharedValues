using UnityEngine;

namespace SharedValues
{
    public abstract class StringConverterBase<T, SVR> : MonoBehaviour
    where SVR : SharedValueReference<T>
    {
        [SerializeField] private SVR value;
        [SerializeField] private SharedStringReference stringValue;

        void OnEnable()
        {
            value.AddListener(ConvertToString);
        }

        void OnDisable()
        {
            value.RemoveListener(ConvertToString);
        }

        private void ConvertToString(T value)
        {
            stringValue.Value = value.ToString();
        }
    }
}