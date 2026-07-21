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