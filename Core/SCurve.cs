using UnityEngine;
using System;

namespace SharedValues
{
    [Serializable]
    public struct SCurve
    {
        [Tooltip("The minimum value that can be returned")]
        [SerializeField,Min(0)] public float min;

        [Tooltip("The maximum value that can be returned")]
        [SerializeField,Min(0)] public float max;

        
        [Tooltip("The speed at which the curve goes from exponential to logarithmic")]
        [SerializeField, Min(0)] public float slope;

        [Tooltip("The point at which the curve goes from exponential to logarithmic")]
        [SerializeField] public float InflectionPoint;

        public float Evaluate(float t, bool zCurve = false)
        {
            return Evaluate(this, t, zCurve);
        }

        public static float Evaluate(SCurve curveValues, float t, bool zCurve = false)
        {
            return Evaluate(curveValues.min, curveValues.max, curveValues.InflectionPoint, t, curveValues.slope, zCurve);
        }

        public static float Evaluate(float min, float max, float inflectionPoint, float t, float curveSlope = 1, bool zCurve = false)
        {
            float numerator = max - min;
            float denominator = 1 + Mathf.Exp((zCurve ? -curveSlope : curveSlope) * (inflectionPoint - t));
            float value = numerator / denominator + min;
            return value;
        }
    }
}