using UnityEngine;
using UnityEditor;
using SharedValues.Attributes;

namespace SharedValues.Editor
{
    [CustomPropertyDrawer(typeof(CurveRange))]
    public class CurveRangePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CurveRange curve = attribute as CurveRange;

            // This attribute should only be used with AnimationCurves
            if (property.propertyType != SerializedPropertyType.AnimationCurve)
                throw new System.Exception("AnimationCurveSimple attribute can only be used with AnimationCurve properties, not " + property.propertyType);

            // Check if the property has an assigned AnimationCurve. Create one if it doesn't.
            // If it does, then make sure it has at least 2 keys to avoid empty curves in the inspector.
            if (property.animationCurveValue == null ? true : property.animationCurveValue.keys.Length <= 1)
                property.animationCurveValue = new AnimationCurve(new Keyframe(curve.bbox.xMin, curve.bbox.center.y), new Keyframe(curve.bbox.xMax, curve.bbox.center.y));
        
            // Draw the curve in the inspector (this is not the popup editor window)
            EditorGUI.CurveField(position, property, curve.color, curve.bbox, label);
        }
    }
}