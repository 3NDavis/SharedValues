using UnityEngine;

namespace SharedValues.Averages
{
    [CreateAssetMenu(menuName = "Shared Values/Averages/Float", fileName = "SharedVal_Averaged_Float_Name")]
    public sealed class AverageFloat : AverageValue<SharedFloatReference, float>
    {
        protected override string GetTextureName()
        {
            return "Float";
        }

        protected override void RecalculateAverage(float newValue)
        {
            float total = 0;
            for (int i = 0; i < ValuesToAverage.Count; i++)
            {
                total += ValuesToAverage[i].Value;
            }
            SetAverage(total / ValuesToAverage.Count);
        }
    }
}