using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DataDropdownAttribute))]
public class DataDropdownPropertyDrawer : PropertyDrawer
{
    private List<ScriptableObject> objects;
    private string[] names;
    private int index;
    private int id;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var dataDropdown = attribute as DataDropdownAttribute;
        var type = dataDropdown.type;
        var dataPath = dataDropdown.path;

        var types = AssetDatabase.FindAssets("t:" + type.ToString(), new[] { dataPath });

        if (types.Length == 0)
        {
            var half = position.width / 2;
            var rect1 = new Rect(position.x, position.y, half, position.height);
            var rect2 = new Rect(half, position.y, half, position.height);

            EditorGUI.LabelField(rect1, property.displayName);
            EditorGUI.LabelField(rect2, "No suitable data found");
        }
        else
        {
            if (objects == null)
            {
                objects = new List<ScriptableObject>();
                names = new string[types.Length];
                int i = 0;

                foreach (var guid in types)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var obj = AssetDatabase.LoadAssetAtPath(path, type) as ScriptableObject;
                    objects.Add(obj);
                    names[i] = obj.name;
                    i++;
                }

                if (property.objectReferenceValue != null)
                {
                    var t = property.objectReferenceValue as ScriptableObject;
                    id = objects.IndexOf(t);
                }
            }

            id = EditorGUI.Popup(position, property.displayName, id, names);
            if (id != index)
            {
                index = id;
                property.objectReferenceValue = objects[index];
                property.serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
