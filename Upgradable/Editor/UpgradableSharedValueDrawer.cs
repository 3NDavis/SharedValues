
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
   
   
   
using SharedValues.Upgradable;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SharedValues.Upgradable.Editor
{
    [CustomPropertyDrawer(typeof(UpgradableSharedValue), true)]
    public class UpgradableSharedValueDrawer : PropertyDrawer
    {
        public VisualTreeAsset propertyUXML;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement myProperty = new VisualElement();
            VisualElement uxmlContent = propertyUXML.CloneTree();

            var outLabelProp = uxmlContent.Q<PropertyField>("BaseValue");
            outLabelProp.label = property.displayName;

            myProperty.Add(uxmlContent);

            return myProperty;
        }
    }
}

