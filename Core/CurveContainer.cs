using UnityEngine;
using SharedValues.Attributes;

namespace SharedValues
{
    [System.Serializable]
    public struct CurveContainer
    {
        [SerializeField] private CurveType curveType;
        bool useConstant => curveType == CurveType.Constant || curveType == CurveType.ConstantAccelerated;

        [Visibility(nameof(useConstant), true)]
        [SerializeField] private float constant;

        [Visibility(nameof(curveType), CurveType.ConstantAccelerated)]
        [SerializeField] private float acceleration;


        [CurveRange(0, -20, 20, 20)]
        [Visibility(nameof(curveType), CurveType.Curve)]
        [SerializeField] private AnimationCurve curve;

        bool showSCurve => curveType == CurveType.sCurve || curveType == CurveType.zCurve;
        [Visibility(nameof(showSCurve), true)]
        [SerializeField] private SCurve sCurve;

        enum CurveType
        {
            None,
            Constant,
            ConstantAccelerated,
            Curve,
            sCurve,
            zCurve
        }


/// <summary>
/// Returns the value of the function at the specified time.
/// </summary>
/// <param name="time">the time at which the set function should be evaluated</param>
/// <param name="defaultToOne">if the result of the function is < 1, set it to 1</param>
/// <returns></returns>
        public float Evaluate(float time, bool defaultToOne = false)
        {
            switch (curveType)
            {
                case CurveType.Constant:
                    return defaultToOne ? Mathf.Max(1,constant) : constant;
                case CurveType.ConstantAccelerated:
                    return constant + acceleration * time;
                case CurveType.Curve:
                    if(curve != null)
                        return defaultToOne ? Mathf.Max(1,curve.Evaluate(time)) : curve.Evaluate(time);
                    return 1;
                case CurveType.sCurve:
                    return sCurve.Evaluate(time);
                case CurveType.zCurve:
                    return sCurve.Evaluate(time, true);
                case CurveType.None:
                    return defaultToOne ? 1 : 0;
                    
                default:
                    return 1;
            }
        }
    }
}