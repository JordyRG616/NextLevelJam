using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class FakeCursor : MonoBehaviour
{
    private Vector2 canvasResolution;
    private RectTransform self => transform as RectTransform;
    private Mouse mouse;


    private void Start()
    {
        var canvas = transform.root.GetComponent<CanvasScaler>();
        canvasResolution = canvas.referenceResolution;

        mouse = Mouse.current;
    }

    private void OnEnable()
    {
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        var pos = mouse.position.ReadValue();

        var currentResolution = Screen.currentResolution;
        pos.x /= currentResolution.width;
        pos.x *= canvasResolution.x;
        pos.y /= currentResolution.height;
        pos.y *= canvasResolution.y;

        self.anchoredPosition = pos;
    }
}
