using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SignalExplorer : EditorWindow
{
    private static List<string> adresses = new List<string>();
    private Vector2 scrollPosition;

    [MenuItem("Fracta/Signal Explorer")]
    public static void OpenWindow()
    {
        GetWindow<SignalExplorer>();

        GetSignals();
    }

    private static void GetSignals()
    {
        adresses.Clear();
        var objects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID);

        foreach(var obj in objects)
        {
            Debug.Log(obj.GetType().GetFields().Length);
            foreach(var prop in obj.GetType().GetFields())
            {
                if(prop.FieldType.GetInterface("ISignal") != null)
                {
                    var adress = obj.name + "/" + obj.GetType() + "/" + prop.Name;
                    adresses.Add(adress);
                }
            }
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.MaxWidth(250), GUILayout.ExpandHeight(true));
                
                foreach (var adress in adresses)
                {
                    EditorGUILayout.LabelField(adress);
                }


            EditorGUILayout.EndScrollView();


            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandHeight(true));
                EditorGUILayout.LabelField("test");

            EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
    }
}
