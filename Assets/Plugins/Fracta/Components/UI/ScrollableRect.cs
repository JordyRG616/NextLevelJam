using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(ContentSizeFitter))]
[AddComponentMenu("UI/Scrollable Rect")]
public class ScrollableRect : MonoBehaviour
{
    [SerializeField] private RectTransform parent;
    [SerializeField] private Scrollbar scrollbar;

    private RectTransform rectTransform => transform as RectTransform;
    private float delta
    {
        get
        {
            if (rectTransform.sizeDelta.y <= parent.sizeDelta.y)
            {
                scrollbar.gameObject.SetActive(false);
                return 0;
            }
            else
            {
                scrollbar.gameObject.SetActive(true);
                return rectTransform.sizeDelta.y - parent.sizeDelta.y;
            }
        }
    }


    private void OnEnable()
    {
        SetPosition(0);
    }

    public void SetPosition(float pos)
    {
        var y = Mathf.Lerp(0, delta, pos);

        rectTransform.anchoredPosition = Vector2.up * y;
    }

    private void Update()
    {
        var container = delta;
    }
}
