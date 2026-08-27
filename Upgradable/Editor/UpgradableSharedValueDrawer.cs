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
