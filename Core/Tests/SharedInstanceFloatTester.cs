
   //Copyright 2026 Ethan Davis

   //Licensed under the Apache License, Version 2.0 (the "License");
   //you may not use this file except in compliance with the License.
   //You may obtain a copy of the License at
   //  http://www.apache.org/licenses/LICENSE-2.0

   //Unless required by applicable law or agreed to in writing, software
   //distributed under the License is distributed on an "AS IS" BASIS,
   //WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   //See the License for the specific language governing permissions and
   //limitations under the License.
   
   
   
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
