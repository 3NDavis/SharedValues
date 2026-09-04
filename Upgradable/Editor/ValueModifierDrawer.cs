
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
using SharedValues.Upgradable;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SharedValues.Upgradable.Editor
{
    [CustomEditor(typeof(ValueModifier), true)]
    public class ValueModifierDrawer : UnityEditor.Editor
    {
        public VisualTreeAsset propertyUXML;

        EnumField upgradeTypeField;
        SerializedProperty upgradeProperty;

        EnumField vectorModTypeField;

        FloatField sCurveField;
        CurveField animCurveField;

    void OnEnable()
    {
        upgradeProperty = serializedObject.FindProperty("modificationType");
    }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement myProperty = new VisualElement();
            VisualElement uxmlContent = propertyUXML.CloneTree();

            sCurveField = uxmlContent.Q<FloatField>("sCurveSlope");
            animCurveField = uxmlContent.Q<CurveField>("animCurve");

            upgradeTypeField = uxmlContent.Q<EnumField>("modificationType");
            upgradeTypeField.RegisterCallback<ChangeEvent<Enum>>(ShowComplexValues);

            vectorModTypeField = uxmlContent.Q<EnumField>("vectorModType");
            vectorModTypeField.style.display = serializedObject.targetObject is VectorValueModifier ? 
                DisplayStyle.Flex : DisplayStyle.None;


            ShowComplexValues();

            myProperty.Add(uxmlContent);

            return myProperty;
        }

        private void ShowComplexValues(ChangeEvent<Enum> evt)
        {
            ShowComplexValues();
        }

        private void ShowComplexValues()
        {
            int index = upgradeProperty.enumValueIndex; 

            sCurveField.style.display = index == 6 || index == 7 ? DisplayStyle.Flex : DisplayStyle.None;

            animCurveField.style.display = index == 8 ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}

