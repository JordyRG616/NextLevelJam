using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WorldLayoutComponent : MonoBehaviour
{
    [SerializeField] private float spacing;

    private void Update()
    {
        var count = transform.childCount;
        if (count == 0) return;

        var space = (spacing * (count - 1)) + count;
        var distance = space / count;

        for (int i = 0; i < count; i++)
        {
            transform.GetChild(i).localPosition = ((Vector3.left * space) / 2) + (Vector3.right * i * distance);
        }

    }
}
