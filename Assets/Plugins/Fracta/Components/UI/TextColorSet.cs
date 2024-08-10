using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[AddComponentMenu("UI/Color set (TMpro)")]
[RequireComponent(typeof(TextMeshProUGUI))]
public class TextColorSet : MonoBehaviour
{
    [SerializeField] private List<ColorSetEntry> colorSet;

    private TextMeshProUGUI text;
    private Dictionary<string, Color> colorMatrix = new Dictionary<string, Color>();

    public void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        foreach (var entry in colorSet)
        {
            colorMatrix.Add(entry.key, entry.color);
        }
    }

    public void Set(int index)
    {
        text.color = colorSet[index].color;
    }

    public void Set(string key)
    {
        text.color = colorMatrix[key];
    }
}

[System.Serializable]
public struct ColorSetEntry
{
    public string key;
    public Color color;
}