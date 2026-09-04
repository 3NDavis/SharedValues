
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

namespace SharedValues.Attributes
{
    /// <summary>
    /// Forces an AnimationCurve to keep its keys within a certain window. The default is a range of (x0, y0) = (0, 0) to (x1, y1) = (1, 1).
    /// </summary>
    public class CurveRange : PropertyAttribute
    {
        public Rect bbox = new Rect(0f, 0f, 1f, 1f);
        public Color color = Color.green;

        public CurveRange() { }

        public CurveRange(Rect bbox)
        {
            this.bbox = bbox;
        }

        public CurveRange(float xmin, float xmax, float ymin, float ymax)
        {
            bbox = new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
        }

        public CurveRange(Color color)
        {
            bbox = new Rect(0f, 0f, 1f, 1f);
            this.color = color;
        }
    
        public CurveRange(float xmin, float xmax, float ymin, float ymax, Color color)
        {
            bbox = new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
            this.color = color;
        }
    
        public CurveRange(Rect bbox, Color color)
        {
            this.bbox = bbox;
            this.color = color;
        }
    }
}
