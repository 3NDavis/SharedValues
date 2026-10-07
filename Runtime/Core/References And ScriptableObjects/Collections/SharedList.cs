
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

namespace SharedValues.Core.Collections
{
    public class SharedList<TValue> : SharedCollection<List<TValue>, TValue, int, TValue>
    {
        protected override string GetTextureName()
        {
            return "List";
        }

        public sealed override TValue GetValue(int key)
        {
            return Value[key];
        }

        public sealed override bool TryGetValue(int index, out TValue value)
        {
            if(index >= this.Value.Count)
            {
                value = default;
                return false;
            }

            value = this.Value[index];
            return true;
        }

        public sealed override void AddWithoutNotify(int key, TValue value)
        {
            Value.Add(value);
        }

        public override void AddWithoutNotify(TValue collectionItem)
        {
            Value.Add(collectionItem);
        }

        public sealed override void RemoveAtWithoutNotify(int key)
        {
            Value.RemoveAt(key);
        }

        public override void RemoveWithoutNotify(TValue collectionItem)
        {
            Value.Remove(collectionItem);
        }
        
        public sealed override void SetValueWithoutNotify(int index, TValue value)
        {
            Value[index] = value;
        }

        public sealed override void ResetCollection()
        {
            if(Value == null)
            {
                Value = new List<TValue>();
            }
            else
            {
                Value.Clear();
            }
        }

        public sealed override int ConvertCollectionItemToKey(TValue collectionItem)
        {
            return Value.IndexOf(collectionItem);
        }

        public sealed override TValue ConvertCollectionItemToValue(TValue collectionItem)
        {
            return collectionItem;
        }


    }

    public class SharedListReference<TValue> : SharedCollectionReference<List<TValue>, TValue, int, TValue>
    {
        public sealed override TValue GetValue(int index)
        {
            return Value[index];
        }

        public sealed override bool TryGetValue(int index, out TValue value)
        {
            if(Value.Count >= index)
            {
                value = default;
                return false;
            }

            value = Value[index];
            return true;
        }

        public sealed override void AddWithoutNotify(int key, TValue value)
        {
            Value.Add(value);
        }

        public sealed override void AddWithoutNotify(TValue collectionItem)
        {
            Value.Add(collectionItem);
        }

        public sealed override void RemoveAtWithoutNotify(int key)
        {
            Value.RemoveAt(key);
        }

        public sealed override void RemoveWithoutNotify(TValue collectionItem)
        {
            Value.Remove(collectionItem);
        }


        public sealed override void SetValueWithoutNotify(int index, TValue value)
        {
            Value[index] = value;
        }

        public sealed override void ResetCollection()
        {
            if(Value == null)
            {
                Value = new List<TValue>();
            }
            else
            {
                Value.Clear();
            }
        }

        public sealed override int ConvertCollectionItemToKey(TValue collectionItem)
        {
            return Value.IndexOf(collectionItem);
        }

        public sealed override TValue ConvertCollectionItemToValue(TValue collectionItem)
        {
            return collectionItem;
        }
    }
}
