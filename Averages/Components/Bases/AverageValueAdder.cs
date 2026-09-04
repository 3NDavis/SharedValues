using UnityEngine;

namespace SharedValues.Averages
{
    public abstract class AverageValueAdder<A,T,S> : MonoBehaviour
    where A : AverageValue<T,S> where T : SharedValueReference<S> where S : struct
    {
        [SerializeField] private A averagedValue;
        [SerializeField] private T valueToAverage;

        private void Awake() 
        {
            AddToList(valueToAverage);
        }

        public void AddToList(T newReference)
        {
            averagedValue.AddReferenceToAverage(newReference);
        }
    }
}