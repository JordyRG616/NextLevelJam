using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[AddComponentMenu("UI/Color set (image)")]
[RequireComponent(typeof(Image))]
public class ImageColorSet : MonoBehaviour
{
    [SerializeField] private List<ColorSetEntry> colorSet;

    private Image image;
    private Dictionary<string, Color> colorMatrix = new Dictionary<string, Color>();

    public void Start()
    {
        image = GetComponent<Image>();

        foreach (var entry in colorSet)
        {
            colorMatrix.Add(entry.key, entry.color);
        }
    }

    public void Set(int index)
    {
        image.color = colorSet[index].color;
    }

    public void Set(string key)
    {
        image.color = colorMatrix[key];
    }
}
