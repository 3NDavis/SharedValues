
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
   
   
   
using System.Collections;

namespace SharedValues.Enumerators
{
    public abstract class SharedEnumerator<TEnumerator, TItem, TOutItem, TIndexer> : SharedValue<TEnumerator>
    where TEnumerator : ICollection, IEnumerable
    {
        public int Count(){return Value.Count;}
        public abstract void AddToEnumerator(TItem item);
        public abstract void RemoveFromEnumerator(TItem item);
        public abstract void SetEnumeratorValue(TIndexer index, TItem value);
        public abstract bool TryGetEnumeratedValue(TIndexer index, out TOutItem value);
        public abstract void ResetEnumerator();
    }

    public abstract class SharedEnumeratorReference<TEnumerator, TItem, TOutItem, TIndexer> : SharedValueReference<TEnumerator>
    where TEnumerator : ICollection, IEnumerable
    {
        public int Count(){return Value.Count;}
        public abstract void AddToEnumerator(TItem item);
        public abstract void AddToEnumeratorWithoutNotify(TItem item);

        public abstract void RemoveFromEnumerator(TItem item);
        public abstract void RemoveFromEnumeratorWithoutNotify(TItem item);

        public abstract void SetEnumeratedValue(TIndexer index, TOutItem value);
        public abstract void SetEnumeratorValueWithoutNotify(TIndexer index, TItem value);

        public abstract bool TryGetEnumeratatedValue(TIndexer index, out TOutItem value);

        public abstract void ResetEnumerator();

        protected void BroadcastToReference()
        {
            switch (_ReferenceType)
            {
                case ReferenceType.global:
                    _SharedReference.BroadcastValueChange();
                    break;
                case ReferenceType.instanced:
                    SharedValue<TEnumerator> castSharedVal = (SharedValue<TEnumerator>)_instanceGroup.GetInstance(_SharedReference);
                    castSharedVal.BroadcastValueChange();
                    break;
            }
        }
    }
}
