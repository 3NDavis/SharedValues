
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
        public abstract void BroadcastValueChange();

        protected override string GetFilePath()
        {
            return k_sharedValueFilePath + "Core\\";
        }
    }

    public abstract class SharedValue<T> : SharedValue, IValueSpecialSetter<T>, IValueEventHandler<T>
    {
        [SerializeField] private T value;
        public T Value { get => value; set => SetValue(value); }
        private void SetValue(T newValue) { value = newValue; onValueChange?.Invoke(newValue); }
        public void SetValue(SharedValue<T> newValue) {SetValue(newValue.value);}
        public void SetToDefault(){Value = default;}
        public void SetValueWithoutNotify(T newValue) { value = newValue; }
        public void SetValueWithoutNotify(SharedValue<T> newValue) { SetValueWithoutNotify(newValue.value); }
        public event Action<T> onValueChange;

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
            throw new NotImplementedException();
        }

        public void RemoveListener(Action<T> action)
        {
            throw new NotImplementedException();
        }
    }
}
