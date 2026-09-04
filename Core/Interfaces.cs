
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

namespace SharedValues
{
    public interface IValueEventHandler<T>
    {
        public void AddListener(Action<T> action);
        public void RemoveListener(Action<T> action);
    }

    public interface IValueSetter<T>
    {
        public T Value {get; set;}
    }

    public interface IValueSpecialSetter<T> : IValueSetter<T>
    {
        public void SetValueWithoutNotify(T value);
        public void SetValueWithoutNotify(SharedValue<T> value)
        {
            SetValueWithoutNotify(value.Value);
        }
        public void SetValueWithBroadcastIfChange(T value)
        {
            if(!value.Equals(Value))
            {
                Value = value;
            }
        }
        public void SetValueWithBroadcastIfChange(SharedValue<T> value)
        {
            SetValueWithBroadcastIfChange(value.Value);
        }
    }
}
