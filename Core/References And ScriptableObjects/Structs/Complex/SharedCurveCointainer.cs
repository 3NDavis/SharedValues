
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
