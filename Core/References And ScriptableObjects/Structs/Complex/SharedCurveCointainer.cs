using System;
using UnityEngine;

namespace SharedValues
{
    [CreateAssetMenu(menuName = "Shared Values/Variables/Complex/Curve", fileName = "SharedVal_Curve_Name")]
    public class SharedCurveCointainer : SharedStruct<CurveContainer>
    {
        protected override string GetTextureName()
        {
            return "Curve";
        }

        public float Evaluate(float t)
        {
            return Value.Evaluate(t);
        }
    }

    [Serializable]
    public class SharedCurveCointainerReference : SharedValueReference<CurveContainer>
    {
        public float Evaluate(float t)
        {
            return Value.Evaluate(t);
        }
    }
}