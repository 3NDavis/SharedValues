namespace SharedValues
{
    public class FloatValueComparerListener : ValueComparerListener<float, SharedFloatReference>
    {
        protected override bool ComplexCompare(float newValue, float compareValue)
        {
            if((p_compareType & CompareType.greater) == CompareType.greater)
            {
                return newValue > compareValue;
            }
            if((p_compareType & CompareType.less) == CompareType.less)
            {
                return newValue < compareValue;
            }

            return false;
        }
    }
}
