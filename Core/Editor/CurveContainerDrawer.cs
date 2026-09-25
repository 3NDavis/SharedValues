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
