using UnityEngine;

namespace SharedValues
{
    public class SharedInstanceFloatTester : MonoBehaviour
    {
        [SerializeField] string message;
        [SerializeField] SharedFloatReference sharedFloat;
        [SerializeField] SharedVector2Reference sharedVector2;
        [SerializeField] float valueChange;
        [SerializeField] bool changeVal;

        void Awake()
        {
            InvokeRepeating("UpdateValue", 0, 2f);
        }

        public void UpdateValue()
        {
            if (valueChange == 0)
                return;
            if (!changeVal)
                return;
            sharedFloat.Value += valueChange;
            sharedVector2.Value = new Vector2(sharedVector2.Value.x + valueChange, sharedVector2.Value.y + valueChange);
        }

        void OnEnable()
        {
            if (!didStart)
                return;
            sharedFloat.AddListener(SendMessage);
        }

        private void Start()
        {
            OnEnable();
        }

        private void OnDisable()
        {
            sharedFloat.RemoveListener(SendMessage);
        }

        void SendMessage(float value)
        {
#if UnityEditor || DEVELOPMENT_BUILD
            Debug.Log(message + sharedFloat.Value);
            //Debug.Log(message + sharedVector2.Value);
#endif
        }
    }
}