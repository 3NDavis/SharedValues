
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
