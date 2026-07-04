using UnityEngine;

namespace SharedValues.Averages
{
    [CreateAssetMenu(menuName = "Shared Values/Averages/Vector2", fileName = "SharedVal_Averaged_Vector2_Name")]
    public class AverageVector2 : AverageValue<SharedVector2Reference, Vector2>
    {
        protected override void RecalculateAverage(Vector2 newValue)
        {
            Vector2 total = Vector2.zero;
            for (int i = 0; i < ValuesToAverage.Count; i++)
            {
                total += ValuesToAverage[i].Value;
            }
            SetAverage(total / ValuesToAverage.Count);
        }
    }
}