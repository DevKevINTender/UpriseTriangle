
    using Array2DEditor;
    using Services;
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(StaticEnumField))]
    public class StaticEnumFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var prev = GUI.backgroundColor;
            EditorGUI.BeginProperty(position, label, property);
            //GUI.backgroundColor = Color.red;
            
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            if (property.propertyType == SerializedPropertyType.Enum)
            {
                if (property.enumValueIndex == 0)
                {
                    GUI.backgroundColor = Color.white;
                }
                if (property.enumValueIndex == 1)
                {
                    GUI.backgroundColor = Color.cyan;
                }
                if (property.enumValueIndex == 2)
                {
                    GUI.backgroundColor = Color.yellow;
                }
                
            }

            EditorGUI.PropertyField(position, property, label);
            
            EditorGUI.indentLevel = indent;
            
            EditorGUI.EndProperty();
            GUI.backgroundColor = prev;

        }
        public override bool CanCacheInspectorGUI(SerializedProperty property)
        {
            return false;
        }
    }
