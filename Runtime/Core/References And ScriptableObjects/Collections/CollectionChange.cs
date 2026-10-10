
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



namespace SharedValues.Core.Collections
{
#if UNITY_EDITOR
    /// <summary>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// This should not be set in the inspector, it will result in differences between builds and editor
    [System.Serializable]
    public struct CollectionChange<TKey, TValue>
    {
        public CollectionModificationType modificationType;
        public TKey key;
        public TValue value;

        public CollectionChange(CollectionModificationType modificationType, TKey key, TValue value)
        {
            this.modificationType = modificationType;
            this.key = key;
            this.value = value;
        }
    }
#else
    public readonly struct CollectionChange<TKey, TValue>
    {
        public readonly CollectionModificationType modificationType;
        public readonly TKey key;
        public readonly TValue value;

        public CollectionChange(CollectionModificationType modificationType, TKey key, TValue value)
        {
            this.modificationType = modificationType;
            this.key = key;
            this.value = value;
        }
    }
#endif
}