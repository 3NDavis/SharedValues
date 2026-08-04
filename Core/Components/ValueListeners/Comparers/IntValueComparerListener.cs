namespace SharedValues
{
    public class IntValueComparerListener : ValueComparerListener<int, SharedIntReference>
    {
        protected override bool ComplexCompare(int newValue, int compareValue)
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
