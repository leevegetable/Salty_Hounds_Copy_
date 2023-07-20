using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using JetBrains.Annotations;
using System;
using static Cinemachine.CinemachineBlendDefinition;

[CustomEditor(typeof(GameEvent))]
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
        GiveCharacterFeelings = serializedObject.FindProperty("giveCharacterFeelings");
        RewardCharacterFeelings = serializedObject.FindProperty("rewardCharacterFeelings");
        GiveStats = serializedObject.FindProperty("giveStats");
        RewardStats = serializedObject.FindProperty("rewardStats");
        GiveGold = serializedObject.FindProperty("giveGold");
        RewardGold = serializedObject.FindProperty("rewardGold");
        EventActions = serializedObject.FindProperty("EventActions");
        EventActions = serializedObject.FindProperty(nameof(GameEvent.EventActions));
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


        EditorGUILayout.Space(4);
        EditorGUILayout.BeginVertical(Style);
        EditorGUILayout.Space(2);
        EditorGUILayout.LabelField("이벤트 종료 후 리워드");
        EditorGUI.indentLevel += 1;
        GiveCharacterFeelings.boolValue = EditorGUILayout.ToggleLeft("GiveCharacterFeelings", GiveCharacterFeelings.boolValue);
        if (GiveCharacterFeelings.boolValue)
        {
            EditorGUI.indentLevel += 1;
            EditorGUILayout.PropertyField(RewardCharacterFeelings);
            EditorGUILayout.Space(2);
            EditorGUI.indentLevel -= 1;
        }
        GiveStats.boolValue = EditorGUILayout.ToggleLeft("GiveStats", GiveStats.boolValue);
        if (GiveStats.boolValue)
        {
            EditorGUI.indentLevel += 1;
            EditorGUILayout.PropertyField(RewardStats);
            EditorGUILayout.Space(2);
            EditorGUI.indentLevel -= 1;
        }
        GiveGold.boolValue = EditorGUILayout.ToggleLeft("GiveGold", GiveGold.boolValue);
        if (GiveGold.boolValue)
        {
            EditorGUI.indentLevel += 1;
            EditorGUILayout.PropertyField(RewardGold);
            EditorGUILayout.Space(2);
            EditorGUI.indentLevel -= 1;
        }
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();


        EditorGUILayout.Space(8);
        EditorGUILayout.BeginVertical(Style);
        EditorGUILayout.Space(2);
        EditorGUILayout.LabelField("이벤트 발동 내용");
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel += 1;
        ShowEventActions();
        for (int i = 0; i < EventActions.arraySize; i++)
        {
            EditorGUILayout.BeginVertical(Style);
            EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("ActionType"));
            EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("isFold").boolValue = 
                EditorGUILayout.Foldout(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("isFold").boolValue,
                "[Show Detail]", true);
            if (EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("isFold").boolValue)
            {
                EditorGUI.indentLevel += 1;
                switch ((EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("ActionType").enumValueIndex))
                {
                    case 0:
                        EditorGUILayout.LabelField("Update Soon");
                        break;
                    case 1:
                        EditorGUILayout.LabelField("h = Hour, m = Minute, Current Time + h:m");
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("h"));
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("m"));
                        break;
                    case 2:
                        EditorGUILayout.LabelField("Spawn TargetID, Player Current Map");
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("targetID"));
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("spawnPos"));
                        break;
                    case 3:
                        EditorGUILayout.LabelField("Delete TargetID, Player Current Map");
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("targetID"));
                        break;
                    case 4:
                        EditorGUILayout.LabelField("Target Move,Target is Player Current Map Move StartPos to EndPos");
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("TargetType"));
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("startPosCurrent"));
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("startPos"));
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("endPos"));
                        break;
                    case 5:
                        EditorGUILayout.LabelField("Player Change Map");
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("mapCode"));
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("mapPos"));
                        break;
                    case 6:
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("DialogueType"));
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("DialogueID"));
                        break;
                    case 7:
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("CallingType"));
                        EditorGUILayout.PropertyField(EventActions.GetArrayElementAtIndex(i).FindPropertyRelative("CallingID"));
                        break;

                }
                EditorGUI.indentLevel -= 1;
            }

            EditorGUILayout.EndVertical();
        }
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(2);
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
