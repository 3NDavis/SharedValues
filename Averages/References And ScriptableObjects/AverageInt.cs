using UnityEngine;

namespace SharedValues.Averages
{
    [CreateAssetMenu(menuName = "Shared Values/Averages/Int", fileName = "SharedVal_Averaged_Int_Name")]
    public class AverageInt : AverageValue<SharedIntReference, int>
    {
        protected override string GetTextureName()
        {
            return "Int";
        }

        protected override void RecalculateAverage(int newValue)
        {
            float total = 0;
            for (int i = 0; i < ValuesToAverage.Count; i++)
            {
                total += ValuesToAverage[i].Value;
            }
            SetAverage(Mathf.RoundToInt(total / ValuesToAverage.Count));
        }
    }
}