using System;
using System.Diagnostics;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace SharedValues.Editor
{
    [CustomPropertyDrawer(typeof(SharedValueReference), true)]
    public class SharedValueReferenceDrawer : PropertyDrawer
    {
        public VisualTreeAsset propertyUXML;

        SerializedProperty referenceTypeProperty;

        PropertyField valueProp;
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


            TabView tabMenu = uxmlContent.Q<TabView>("TabMenu");
            valueProp = uxmlContent.Q<PropertyField>("Value");
            sharedProp = uxmlContent.Q<PropertyField>("SharedValue");
            instanceProp = uxmlContent.Q<PropertyField>("Instancer");
            var refProp = uxmlContent.Q<PropertyField>("ReferenceType");

            tabMenu.tabIndex = referenceTypeProperty.enumValueIndex;

            tabMenu.activeTabChanged += SetReferenceType;
            SetReferenceType(tabMenu.activeTab, tabMenu.activeTab);

            myProperty.Add(uxmlContent);
            return myProperty;
        }

        private void SetReferenceType(Tab tab1, Tab tab2)
        {
            referenceTypeProperty.enumValueIndex = tab2.tabIndex;
            var enumValue = referenceTypeProperty.enumValueIndex;

            valueProp.style.display = enumValue == 0 ? DisplayStyle.Flex : DisplayStyle.None;
            sharedProp.style.display = enumValue > 0 ? DisplayStyle.Flex : DisplayStyle.None;
            instanceProp.style.display = enumValue == 2 ? DisplayStyle.Flex : DisplayStyle.None;
            UnityEngine.Debug.Log(referenceTypeProperty.displayName + " should have been turned to " + tab2.tabIndex + " and is " + referenceTypeProperty.enumValueIndex);
        }
    }
}