using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PivotController : MonoBehaviour
{
    [SerializeField] private float horizontalOffset;
    [SerializeField] private float verticalOffset;

    private RectTransform rect => transform as RectTransform;

    private Vector2 screenSize;
    private Vector2 maxPosition;


    private void Start()
    {
        var res = Screen.currentResolution;

        screenSize.x = 1920;// res.width;
        screenSize.y = 1080;// res.height;

        maxPosition.x = rect.sizeDelta.x * (1 + horizontalOffset);
        maxPosition.y = rect.sizeDelta.y * (1 + verticalOffset);

        gameObject.SetActive(false);
    }

    private void CheckPositionOnScreen()
    {
        Vector2 pivot = new Vector2();

        if (rect.anchoredPosition.x + maxPosition.x > screenSize.x)
        {
            pivot.x = 1 + horizontalOffset;
        } else
        {
            pivot.x = -horizontalOffset;
        }

        if (rect.anchoredPosition.y - maxPosition.y < 0)
        {
            pivot.y = -verticalOffset;
        } else
        {
            pivot.y = 1 + verticalOffset;
        }

        rect.pivot = pivot;
    }

    private void Update()
    {
        rect.anchoredPosition = Mouse.current.position.ReadValue();

        CheckPositionOnScreen();
    }
}
