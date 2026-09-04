
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
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using SharedValues.Events;

namespace SharedValues.Editor
{
    [CustomPropertyDrawer(typeof(SharedEventReference), true)]
    public class SharedEventReferenceDrawer : PropertyDrawer
    {
        public VisualTreeAsset propertyUXML;

        SerializedProperty referenceTypeProperty;

        PropertyField sharedProp;
        PropertyField instanceProp;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement myProperty = new VisualElement();
            
            if(property.type == nameof(SharedValueReference))
            {
                TextElement failText = new TextElement();
                failText.text = "A SharedValueReference cannot be the base class";
                myProperty.Add(failText);
                return myProperty;
            }

            VisualElement uxmlContent = propertyUXML.CloneTree();
            referenceTypeProperty = property.FindPropertyRelative("referenceType");

            var outLabelProp = uxmlContent.Q<TextElement>("OutVarLabel");
            outLabelProp.text = property.displayName;

            var outVarProp = uxmlContent.Q<PropertyField>("OutVar");

            sharedProp = uxmlContent.Q<PropertyField>("SharedEvent");
            instanceProp = uxmlContent.Q<PropertyField>("Instancer");
            var refProp = uxmlContent.Q<EnumField>("ReferenceType");
            refProp.RegisterCallback<ChangeEvent<Enum>>(SetReferenceType);

            SetReferenceType();

            myProperty.Add(uxmlContent);

            return myProperty;
        }

        private void SetReferenceType(ChangeEvent<Enum> evt)
        {
            SetReferenceType();
        }

        private void SetReferenceType()
        {
            instanceProp.style.display = referenceTypeProperty.enumValueIndex == 1 ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
