
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

namespace SharedValues.Core.Enumerators
{
    public class SharedList<TCollectionItem> : SharedCollection<List<TCollectionItem>, TCollectionItem, int, TCollectionItem>
    {
        protected override string GetTextureName()
        {
            return "List";
        }

        public sealed override void ResetCollection()
        {
            if(Value == null)
            {
                Value = new List<TCollectionItem>();
            }
            else
            {
                Value.Clear();
            }
        }

        public sealed override void Add(TCollectionItem collectionItem)
        {
           Value.Add(collectionItem);
        }

        public sealed override void Add(int key, TCollectionItem value)
        {
            Value.Add(value);
        }

        public sealed override void Remove(TCollectionItem collectionItem)
        {
            Value.Remove(collectionItem);
        }

        public override void RemoveAt(int key)
        {
            Value.RemoveAt(key);
        }

        public sealed override TCollectionItem GetValue(int key)
        {
            return Value[key];
        }

        public sealed override void SetValue(int index, TCollectionItem value)
        {
            Value[index] = value;
        }

        public sealed override bool TryGetValue(int index, out TCollectionItem value)
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

    public class SharedListReference<TCollectionItem> : SharedCollectionReference<List<TCollectionItem>, TCollectionItem, int, TCollectionItem>
    {
        public sealed override void Add(TCollectionItem value)
        {
            AddWithoutNotify(value);
            BroadcastToReference();
        }

        public sealed override void Add(int key, TCollectionItem value)
        {
            AddWithoutNotify(value);
            BroadcastToReference();
        }

        public sealed override void AddWithoutNotify(TCollectionItem value)
        {
            Value.Add(value);
        }

        public sealed override void AddWithoutNotify(int key, TCollectionItem value)
        {
            Value.Add(value);
        }

        public sealed override TCollectionItem GetValue(int index)
        {
            return Value[index];
        }

        public sealed override void Remove(TCollectionItem value)
        {
            RemoveWithoutNotify(value);
            BroadcastToReference();
        }

        public override void RemoveAt(int key)
        {
            Value.RemoveAt(key);
        }

        public sealed override void RemoveWithoutNotify(TCollectionItem value)
        {
            Value.Remove(value);
        }

        public override void RemoveAtWithoutNotify(int key)
        {
            Value.RemoveAt(key);
        }

        public sealed override void ResetCollection()
        {
            if(Value == null)
            {
                Value = new List<TCollectionItem>();
            }
            else
            {
                Value.Clear();
            }
            BroadcastToReference();
        }

        public sealed override void SetValue(int index, TCollectionItem value)
        {
            SetValueWithoutNotify(index, value);

            BroadcastToReference();
        }

        public sealed override void SetValueWithoutNotify(int index, TCollectionItem value)
        {
            if(index >= Value.Count)
                return;
            
            Value[index] = value;
        }

        public sealed override bool TryGetValue(int index, out TCollectionItem value)
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
