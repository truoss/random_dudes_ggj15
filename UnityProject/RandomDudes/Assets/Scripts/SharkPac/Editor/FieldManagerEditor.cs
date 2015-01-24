using UnityEngine;
using UnityEditor;
using SharkPac;
using System.Collections;

[CustomEditor(typeof(FieldManager))]
public class FieldManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();

        //Field field = (Field)target;
        FieldManager fieldManager = (FieldManager)target;
        Field[,] field = fieldManager.fields;

        GUILayout.Label("Set Cliffs: ");

        EditorGUILayout.BeginVertical();
        for (int y = field.GetLength(1)-1; y >= 0; y--)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            for (int x = 0; x < field.GetLength(0); x++)
            {
                if (field[x, y] != null)
                    field[x, y].currentState = (Field.FieldState)EditorGUILayout.EnumPopup(field[x, y].currentState);//.Toggle(field[x, y].isBlocked, new GUIContent());
                //unit.moveRange[idx] = GUILayout.Toggle(unit.moveRange[idx], new GUIContent()); idx++;
                
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        /*
        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            for (int i = 0; i < ; i++)
            {
                //unit.moveRange[idx] = GUILayout.Toggle(unit.moveRange[idx], new GUIContent()); idx++;
            }
            GUILayout.FlexibleSpace();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            for (int i = 0; i < 3; i++)
            {
                //unit.moveRange[idx] = GUILayout.Toggle(unit.moveRange[idx], new GUIContent()); idx++;
            }
            GUILayout.FlexibleSpace();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            for (int i = 0; i < 3; i++)
            {
                //unit.moveRange[idx] = GUILayout.Toggle(unit.moveRange[idx], new GUIContent()); idx++;
            }
            GUILayout.FlexibleSpace();
        }
        EditorGUILayout.EndHorizontal();
        */


        GUILayout.Space(16);
        /*
        idx = 0;

        GUILayout.Label("Attack: ");

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            for (int i = 0; i < 5; i++)
            {
                unit.attackRange[idx] = GUILayout.Toggle(unit.attackRange[idx], new GUIContent()); idx++;
            }
            GUILayout.FlexibleSpace();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            for (int i = 0; i < 5; i++)
            {
                unit.attackRange[idx] = GUILayout.Toggle(unit.attackRange[idx], new GUIContent()); idx++;
            }
            GUILayout.FlexibleSpace();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            for (int i = 0; i < 5; i++)
            {
                unit.attackRange[idx] = GUILayout.Toggle(unit.attackRange[idx], new GUIContent()); idx++;
            }
            GUILayout.FlexibleSpace();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            for (int i = 0; i < 5; i++)
            {
                unit.attackRange[idx] = GUILayout.Toggle(unit.attackRange[idx], new GUIContent()); idx++;
            }
            GUILayout.FlexibleSpace();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            for (int i = 0; i < 5; i++)
            {
                unit.attackRange[idx] = GUILayout.Toggle(unit.attackRange[idx], new GUIContent()); idx++;
            }
            GUILayout.FlexibleSpace();
        }
        EditorGUILayout.EndHorizontal();
         * */
    }
}
