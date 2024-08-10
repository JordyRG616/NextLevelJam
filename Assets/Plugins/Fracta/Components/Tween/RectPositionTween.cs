using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectPositionTween : MonoBehaviour
{
    [SerializeField] private AnimationCurve timeCurve;
    [Space]
    [SerializeField] private Vector2 origin;
    [SerializeField] private Vector2 destination;

    private RectTransform rectTransform => transform as RectTransform; 

    public void DoTween(float time)
    {
        var value = timeCurve.Evaluate(Mathf.Clamp01(time));

        var pos = Vector2.Lerp(origin, destination, value);

        rectTransform.anchoredPosition = pos;
    }
}
