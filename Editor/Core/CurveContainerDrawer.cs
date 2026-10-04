
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
   
   
   
using SharedValues.Core.Curves;
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SharedValues.Editor
{
    [CustomPropertyDrawer(typeof(CurveContainer), true)]
    public class CurveContainerDrawer : PropertyDrawer
    {
        public VisualTreeAsset visualTree;

        SerializedProperty curveType;

        FloatField constant;
        FloatField accel;
        CurveField curve;
        PropertyField sCurve;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var doc = visualTree.CloneTree();

            curveType = property.FindPropertyRelative("curveType");

            var outLabelProp = doc.Q<Foldout>("Label");
            outLabelProp.text = property.displayName;

            constant = doc.Q<FloatField>("Constant");
            accel = doc.Q<FloatField>("Acceleration");
            curve = doc.Q<CurveField>("Curve");
            sCurve = doc.Q<PropertyField>("SCurve");

            var type = doc.Q<EnumField>("CurveType");
            type.RegisterCallback<ChangeEvent<Enum>>(UpdateVisibility);

            UpdateVisibility();

            return doc;
        }

        private void UpdateVisibility(ChangeEvent<Enum> evt)
        {
            UpdateVisibility();
        }
        private void UpdateVisibility()
        {
            constant.style.display = (curveType.enumValueIndex == 1 || curveType.enumValueIndex == 2) ? DisplayStyle.Flex : DisplayStyle.None;
            accel.style.display = curveType.enumValueIndex == 2 ? DisplayStyle.Flex : DisplayStyle.None;
            curve.style.display = curveType.enumValueIndex == 3 ? DisplayStyle.Flex : DisplayStyle.None;
            sCurve.style.display = curveType.enumValueIndex > 3 ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
