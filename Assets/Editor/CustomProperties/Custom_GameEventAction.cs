using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(Action_Calling))]
public class Custom_GameEventAction : PropertyDrawer
{
    bool isFold;
    SerializedProperty actionName;
    string thisName;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
        EditorGUI.BeginProperty(position, label, property);
        actionName = property.FindPropertyRelative("ActionType");
        thisName = actionName.enumDisplayNames[actionName.enumValueIndex];
        GUI.BeginGroup(position, thisName);
        EditorGUILayout.LabelField(thisName);
        if (isFold)
        {

        }
        GUI.EndGroup();
        EditorGUI.EndProperty();
    }
}
