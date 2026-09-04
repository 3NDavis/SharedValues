using System;
using System.Collections.Generic;
using UnityEngine;

namespace SharedValues.Averages
{
    public abstract class AverageValue<T, S> : SharedSOBase , ISerializationCallbackReceiver
    where T : SharedValueReference<S> where S : struct
    {
        protected override string GetFilePath()
        {
            return k_sharedValueFilePath + "Averages\\";
        }

        private S average;
        public S Average {get {return average;} protected set {average = value; onAverageRecalculated?.Invoke(value);}}
        
        public Action<S> onAverageRecalculated;

        List<T> valuesToAverage = new();
        protected List<T> ValuesToAverage => valuesToAverage;

        protected void SetAverage(S newAverage)
        {
            Average = newAverage;
        }

        public void AddReferenceToAverage(T newReference)
        {
            if(valuesToAverage.Contains(newReference))
                return;

            valuesToAverage.Add(newReference);
            newReference.AddListener(RecalculateAverage);
            TryRecalculateAverage(default);
        }
        public void RemoveReferenceFromAverage(T reference)
        {
            if(!valuesToAverage.Contains(reference))
                return;

            valuesToAverage.Remove(reference);
            reference.RemoveListener(RecalculateAverage);
            TryRecalculateAverage(default);
        }

        private void TryRecalculateAverage(S newValue)
        {
            if(valuesToAverage == null)
                return;
            if(valuesToAverage.Count == 0)
                return;

            RecalculateAverage(newValue);
        }
        protected abstract void RecalculateAverage(S newValue);

        public void ClearList()
        {
            if(valuesToAverage == null)
                return;
                
            valuesToAverage.Clear();
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.Log($"The average value list of values was cleared");
            #endif
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            average = default;
            ClearList();
        }
    }
}