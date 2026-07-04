using System.Collections;

namespace SharedValues.Enumerators
{
    public abstract class SharedEnumerator<TEnumerator, TItem, TOutItem, TIndexer> : SharedValue<TEnumerator>
    where TEnumerator : ICollection, IEnumerable
    {
        public abstract void AddToEnumerator(TItem item);
        public abstract void RemoveFromEnumerator(TItem item);
        public abstract void SetEnumeratorValue(TIndexer index, TItem value);
        public abstract bool TryGetEnumeratedValue(TIndexer index, out TOutItem value);
        public abstract void ResetEnumerator();
    }

    public abstract class SharedEnumeratorReference<TEnumerator, TItem, TOutItem, TIndexer> : SharedValueReference<TEnumerator>
    where TEnumerator : ICollection, IEnumerable
    {
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
                case ReferenceType.superGlobal:
                    _SharedReference.BroadcastValueChange();
                    break;
                case ReferenceType.groupedInstance:
                    SharedValue<TEnumerator> castSharedVal = (SharedValue<TEnumerator>)_instanceGroup.GetInstance(_SharedReference);
                    castSharedVal.BroadcastValueChange();
                    break;
            }
        }
    }
}