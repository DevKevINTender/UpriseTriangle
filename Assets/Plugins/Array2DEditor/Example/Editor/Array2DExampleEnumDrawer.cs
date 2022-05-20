using UnityEditor;
using UnityEngine;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(Array2DExampleEnum))]
    public class Array2DExampleEnumDrawer : Array2DEnumDrawer<GridEnum>
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //EditorGUI.PropertyField(position ,property.FindPropertyRelative("cells"), GUIContent.none);

            
            base.OnGUI(position, property, label);
        }
    }
}
