using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using JetBrains.Annotations;
using System;
using static Cinemachine.CinemachineBlendDefinition;
using UnityEditor.TerrainTools;
using Cinemachine.Editor;

[CustomEditor(typeof(GameEvent),true,isFallback = true)]
[CanEditMultipleObjects]
public class Custom_GameEvent : Editor
{
    private SerializedProperty WorkerName;
    private SerializedProperty TagList;
    private SerializedProperty type;
    private SerializedProperty EventID;
    private SerializedProperty UseCharacterFeelings;
    private SerializedProperty NeedCharacterFeelings;
    private SerializedProperty UseStats;
    private SerializedProperty NeedStats;
    private SerializedProperty UsePlayerMapCode;
    private SerializedProperty NeedPlayerMapCode;
    private SerializedProperty GiveCharacterFeelings;
    private SerializedProperty RewardCharacterFeelings;
    private SerializedProperty GiveStats;
    private SerializedProperty RewardStats;
    private SerializedProperty GiveGold;
    private SerializedProperty RewardGold;
    private SerializedProperty EventActions;
    private string[] Tags;
    private GameEventAction.actionType createType;
    private GameEventAction.ifType IFType;
    private GameEventAction.ifAction IFAction;

    private void OnEnable()
    {
        WorkerName = serializedObject.FindProperty("WorkerName");
        TagList = serializedObject.FindProperty("TagList");
        type = serializedObject.FindProperty("type");
        EventID = serializedObject.FindProperty("eventID");
        UseCharacterFeelings = serializedObject.FindProperty("useCharacterFeelings");
        NeedCharacterFeelings = serializedObject.FindProperty("needCharacterFeelings");
        UseStats = serializedObject.FindProperty("useStats");
        NeedStats = serializedObject.FindProperty("needStats");
        UsePlayerMapCode = serializedObject.FindProperty("usePlayerMapCode");
        NeedPlayerMapCode = serializedObject.FindProperty("needPlayerMapCode");
        EventActions = serializedObject.FindProperty("EventActions");
    }

    public override void OnInspectorGUI()
    {
        GUIStyle Style = EditorStyles.helpBox;
        var Custom = target as GameEvent;
        EditorGUILayout.BeginVertical(Style);
        EditorGUILayout.Space(3);
        EditorGUILayout.BeginVertical(Style);
        EditorGUILayout.Space(2);
        EditorGUILayout.PropertyField(type);
        EditorGUILayout.Space(1);
        EditorGUILayout.PropertyField(EventID);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical(Style);
        EditorGUILayout.Space(2);
        EditorGUILayout.LabelField("이벤트 발동 조건");
        EditorGUI.indentLevel += 1;
        UseCharacterFeelings.boolValue = EditorGUILayout.ToggleLeft("UseCharacterFeelings", UseCharacterFeelings.boolValue);
        if (UseCharacterFeelings.boolValue)
        {
            EditorGUI.indentLevel += 1;
            EditorGUILayout.PropertyField(NeedCharacterFeelings);
            EditorGUILayout.Space(2);
            EditorGUI.indentLevel -= 1;
        }
        UseStats.boolValue = EditorGUILayout.ToggleLeft("UseStats", UseStats.boolValue);
        if (UseStats.boolValue)
        {
            EditorGUI.indentLevel += 1;
            EditorGUILayout.PropertyField(NeedStats);
            EditorGUILayout.Space(2);
            EditorGUI.indentLevel -= 1;
        }
        UsePlayerMapCode.boolValue = EditorGUILayout.ToggleLeft("UsePlayerMapCode", UsePlayerMapCode.boolValue);
        if (UsePlayerMapCode.boolValue)
        {
            EditorGUI.indentLevel += 1;
            EditorGUILayout.PropertyField(NeedPlayerMapCode);
            EditorGUI.indentLevel -= 1;
        }
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(8);
        //EditorGUILayout.BeginVertical(Style);
        //EditorGUILayout.Space(2);
        EditorGUILayout.LabelField("이벤트 발동 내용");
        
        EditorGUILayout.BeginVertical(Style);

        EditorGUILayout.BeginHorizontal();
        createType = (GameEventAction.actionType)EditorGUILayout.EnumPopup(createType);
        if (GUILayout.Button("Create Action"))
        {
            switch (createType)
            {
                case GameEventAction.actionType.PopupMessage:
                    Custom.EventActions.Add(new Action_PopupMessage());
                    break;
                case GameEventAction.actionType.Waiting:
                    Custom.EventActions.Add(new Action_Waiting());
                    break;
                case GameEventAction.actionType.SpawnCharacter:
                    Custom.EventActions.Add(new Action_SpawnCharacter());
                    break;
                case GameEventAction.actionType.DeleteCharacter:
                    Custom.EventActions.Add(new Action_DeleteCharacter());
                    break;
                case GameEventAction.actionType.Move:
                    Custom.EventActions.Add(new Action_CharacterMove());
                    break;
                case GameEventAction.actionType.MapMove:
                    Custom.EventActions.Add(new Action_MapMove());
                    break;
                case GameEventAction.actionType.Dialogue:
                    Custom.EventActions.Add(new Action_Dialogue());
                    break;
                case GameEventAction.actionType.Calling:
                    Custom.EventActions.Add(new Action_Calling());
                    break;
                case GameEventAction.actionType.Choices:
                    Custom.EventActions.Add(new Action_Choices());
                    break;
                case GameEventAction.actionType.FadeOut:
                    Custom.EventActions.Add(new Action_Fadeout());
                    break;
                case GameEventAction.actionType.GiveReward:
                    Custom.EventActions.Add(new Action_GiveReward());
                    break;
            }

            serializedObject.Update();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IFType = (GameEventAction.ifType)EditorGUILayout.EnumPopup(IFType);
        if (IFType != GameEventAction.ifType.none && IFType != GameEventAction.ifType.EndIF && IFType != GameEventAction.ifType.Else)
        {
            IFAction = (GameEventAction.ifAction)EditorGUILayout.EnumPopup(IFAction);
            if (IFAction != GameEventAction.ifAction.none )
            {
                if (GUILayout.Button("Create IF"))
                {
                    switch (IFAction)
                    {
                        case GameEventAction.ifAction.Time:
                            Custom.EventActions.Add(new If_Time(IFType,IFAction));
                            break;
                    }
                    if (IFType == GameEventAction.ifType.IF)
                    {
                        Custom.EventActions.Add(new EndIf_Action());
                    }
                    serializedObject.Update();
                }
            }
        }
        if (IFType == GameEventAction.ifType.Else)
        {
            if (GUILayout.Button("Create Else"))
            {
                Custom.EventActions.Add(new Else_Action(IFType));
                serializedObject.Update();
            }
        }
        EditorGUILayout.EndHorizontal();




        EditorGUI.indentLevel += 1;
        //for (int i = 0; i < EventActions.arraySize; i++)
        //{
        //    SerializedProperty action = serializedObject.FindProperty(EventActions.GetArrayElementAtIndex(i).propertyPath);
        //    EditorGUILayout.PropertyField(action);

        //}
        EditorGUILayout.PropertyField(EventActions);
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(3);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(4);
        EditorGUILayout.BeginVertical(Style);
        EditorGUI.indentLevel += 1;
        EditorGUILayout.PropertyField(WorkerName);
        EditorGUILayout.Space(1);
        EditorGUILayout.PropertyField(TagList);
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.EndVertical();



        if (GUILayout.Button("SaveWorks"))
        {
            Tags = new string[TagList.arraySize + 2];
            Tags[Tags.Length - 2] = WorkerName.enumDisplayNames[WorkerName.enumValueIndex];
            Tags[Tags.Length - 1] = DateTime.Now.ToString(("yyyy.MM.dd.HH.mm"));
            if (TagList.arraySize != 0)
            {
                for (int i = 0; i < TagList.arraySize; i++)
                {
                    Tags[i] = TagList.GetArrayElementAtIndex(i).stringValue;
                }
            }
            List<SerializedProperty> ifactions = new List<SerializedProperty>();
            Debug.Log(Custom.EventActions.Count);
            for (int i = 0; i < Custom.EventActions.Count; i++)
            {
                if (EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("IFType").enumValueIndex == ((int)GameEventAction.ifType.IF))
                {
                    ifactions.Add(EventActions.GetArrayElementAtIndex(i));
                    Debug.Log(ifactions.Count);
                }
                else if (EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("IFType").enumValueIndex == (int)GameEventAction.ifType.ElseIF)
                {
                    ifactions[ifactions.Count - 1].FindPropertyRelative("FalseIndex").intValue = i;
                    ifactions.RemoveAt(ifactions.Count - 1);
                    ifactions.Add(EventActions.GetArrayElementAtIndex(i));
                }
                else if (EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("IFType").enumValueIndex == (int)GameEventAction.ifType.Else)
                {
                    ifactions[ifactions.Count - 1].FindPropertyRelative("FalseIndex").intValue = i;
                    ifactions.RemoveAt(ifactions.Count - 1);
                }
                else if (EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("IFType").enumValueIndex == (int)GameEventAction.ifType.EndIF)
                {
                    ifactions[ifactions.Count - 1].FindPropertyRelative("FalseIndex").intValue = i;
                    ifactions.RemoveAt(ifactions.Count - 1);
                }
                serializedObject.ApplyModifiedProperties();
            }
            ifactions.Clear();

            EditorUtility.SetDirty(target);
            serializedObject.ApplyModifiedProperties();
            AssetDatabase.SetLabels(Custom, Tags);
        }

    }


    private void ShowEventActions()
    {
        var Custom = target as GameEvent;
        if (GUILayout.Button("AddEvent"))
        {
            EventActions.arraySize++;
        }
    }
}
