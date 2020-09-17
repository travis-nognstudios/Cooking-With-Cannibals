using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveState))]
[CanEditMultipleObjects]
public class SaveEditor : Editor
{


    void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SaveState saveState = target as SaveState;
        LevelOneSaveData levelOneSave = target as LevelOneSaveData;

        saveState.debug = EditorGUILayout.Toggle("Debug", saveState.debug);

        if (saveState.debug)
        {
            EditorGUILayout.LabelField("Level One", EditorStyles.boldLabel);
            saveState.setLevelOneCompleted = EditorGUILayout.Toggle("Level One Completed", saveState.setLevelOneCompleted);
            saveState.setLevelOneHG = (Serving.RatingCardSpawner.Rating)EditorGUILayout.EnumPopup("Level One Best Grade", saveState.setLevelOneHG);
            saveState.setLevelOneHS = EditorGUILayout.IntSlider("Level One Highscore", saveState.setLevelOneHS, 0, 20);
            saveState.setLevelOneTime = EditorGUILayout.Slider("Level One Best Time", saveState.setLevelOneTime, 0, 900);

            EditorGUILayout.LabelField("Level Two", EditorStyles.boldLabel);
            saveState.setLevelTwoCompleted = EditorGUILayout.Toggle("Level Two Completed", saveState.setLevelTwoCompleted);
            saveState.setLevelTwoHG = (Serving.RatingCardSpawner.Rating)EditorGUILayout.EnumPopup("Level Two Best Grade", saveState.setLevelTwoHG);
            saveState.setLevelTwoHS = EditorGUILayout.IntSlider("Level Two Highscore", saveState.setLevelTwoHS, 0, 22);
            saveState.setLevelTwoTime = EditorGUILayout.Slider("Level Two Best Time", saveState.setLevelTwoTime, 0, 900);

            EditorGUILayout.LabelField("Level Three", EditorStyles.boldLabel);
            saveState.setLevelThreeCompleted = EditorGUILayout.Toggle("Level Three Completed", saveState.setLevelThreeCompleted);
            saveState.setLevelThreeHG = (Serving.RatingCardSpawner.Rating)EditorGUILayout.EnumPopup("Level Three Best Grade", saveState.setLevelThreeHG);
            saveState.setLevelThreeHS = EditorGUILayout.IntSlider("Level Three Highscore", saveState.setLevelThreeHS, 0, 28);
            saveState.setLevelThreeTime = EditorGUILayout.Slider("Level Three Best Time", saveState.setLevelThreeTime, 0, 900);
        }
    }
}
