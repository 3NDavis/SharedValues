
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
   
   
   
using System;
using UnityEngine;

namespace SharedValues
{
    public abstract class SharedValue : SharedSOBase
    {
        public abstract object objValue{get;}
        public abstract void BroadcastValueChange();

        protected override string GetFilePath()
        {
            return k_sharedValueFilePath + "Core\\";
        }
    }

    public abstract class SharedValue<T> : SharedValue, IValueSpecialSetter<T>, IValueEventHandler<T>
#if UNITY_EDITOR
    , ISerializationCallbackReceiver
#endif

    {
#if UNITY_EDITOR
        public override object objValue => value;
#endif
        [SerializeField] private T value;
        public T Value { get => value; set => SetValue(value); }
        private void SetValue(T newValue) { value = newValue; onValueChange?.Invoke(newValue); }
        public void SetToDefault(){Value = default;}
        public void SetValueWithoutNotify(T newValue) { value = newValue; }
        public event Action<T> onValueChange;

        /// <summary>
        /// Allows the shared value to be used in the place of a value.
        /// Ex sharedInt += 1 instead of sharedInt.Value += 1
        /// </summary>
        /// <param name="sharedValue"></param>
		public static implicit operator T( SharedValue<T> sharedValue )
		{
			return sharedValue.Value;
		}

        //primarily for editor usage
        public override void BroadcastValueChange()
        {
            onValueChange?.Invoke(value);
        }

        protected virtual void OnDestroy()
        {
            if (onValueChange != null)
            {
                var invocationList = onValueChange.GetInvocationList();
                foreach (var invocation in invocationList)
                {
                    onValueChange -= (Action<T>)invocation;
                }
            }
        }

        public void AddListener(Action<T> action)
        {
            onValueChange += action;
        }

        public void RemoveListener(Action<T> action)
        {
            onValueChange -= action;
        }

#if UNITY_EDITOR
        [Header("Editor")]
        [SerializeField] private bool resetToValueOnSerialize;
        [SerializeField] private T valueToResetTo;
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            if (resetToValueOnSerialize)
            {
                value = valueToResetTo;
            }
        }
#endif
    }
}
