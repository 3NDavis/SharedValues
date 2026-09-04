
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
   
   
   
using System.Collections.Generic;

namespace SharedValues.Enumerators
{
    public class SharedList<TItem> : SharedEnumerator<List<TItem>, TItem, TItem, int>
    {
        protected override string GetTextureName()
        {
            return "List";
        }

        public override void ResetEnumerator()
        {
            if(Value == null)
            {
                Value = new List<TItem>();
            }
            else
            {
                Value.Clear();
            }
        }

        public override void AddToEnumerator(TItem value)
        {
           Value.Add(value);
        }

        public override void RemoveFromEnumerator(TItem value)
        {
            Value.Remove(value);
        }

        public override void SetEnumeratorValue(int index, TItem value)
        {
            Value[index] = value;
        }

        public override bool TryGetEnumeratedValue(int index, out TItem value)
        {
            if(index >= this.Value.Count)
            {
                value = default;
                return false;
            }

            value = this.Value[index];
            return true;
        }
    }

    public class SharedListReference<T> : SharedEnumeratorReference<List<T>, T, T, int>
    {
        public override void AddToEnumerator(T value)
        {
            AddToEnumeratorWithoutNotify(value);
            BroadcastToReference();
        }

        public override void AddToEnumeratorWithoutNotify(T value)
        {
            Value.Add(value);
        }

        public override void RemoveFromEnumerator(T value)
        {
            RemoveFromEnumeratorWithoutNotify(value);
            BroadcastToReference();
        }

        public override void RemoveFromEnumeratorWithoutNotify(T value)
        {
            Value.Remove(value);
        }

        public override void ResetEnumerator()
        {
            if(Value == null)
            {
                Value = new List<T>();
            }
            else
            {
                Value.Clear();
            }
            BroadcastToReference();
        }

        public override void SetEnumeratedValue(int index, T value)
        {
            SetEnumeratorValueWithoutNotify(index, value);

            BroadcastToReference();
        }

        public override void SetEnumeratorValueWithoutNotify(int index, T value)
        {
            if(index >= Value.Count)
                return;
            
            Value[index] = value;
        }

        public override bool TryGetEnumeratatedValue(int index, out T value)
        {
            if(Value.Count >= index)
            {
                value = default;
                return false;
            }

            value = Value[index];
            return true;
        }
    }
}
