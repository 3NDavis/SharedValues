
   //Copyright 2026 Ethan Davis

   //Licensed under the Apache License, Version 2.0 (the "License");
   //you may not use this file except in compliance with the License.
   //You may obtain a copy of the License at
   //  http://www.apache.org/licenses/LICENSE-2.0

   //Unless required by applicable law or agreed to in writing, software
   //distributed under the License is distributed on an "AS IS" BASIS,
   //WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   //See the License for the specific language governing permissions and
   //limitations under the License.
   
   
   
using UnityEngine;
using SharedValues.Attributes;

namespace SharedValues
{
    [System.Serializable]
    public struct CurveContainer
    {
        [SerializeField] public CurveType curveType;
        bool useConstant => curveType == CurveType.Constant || curveType == CurveType.ConstantAccelerated;

        //[Visibility(nameof(useConstant), true)]
        [SerializeField] public float constant;

        //[Visibility(nameof(curveType), CurveType.ConstantAccelerated)]
        [SerializeField] public float acceleration;


        [CurveRange(0, -20, 20, 20)]
        //[Visibility(nameof(curveType), CurveType.Curve)]
        [SerializeField] public AnimationCurve curve;

        //[Visibility(nameof(showSCurve), true)]
        [SerializeField] public SCurve sCurve;

        public enum CurveType
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
