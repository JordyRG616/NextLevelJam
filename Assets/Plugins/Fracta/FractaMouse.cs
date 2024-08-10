using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine;

public static class FractaMouse
{
    private static Vector2 resolution = new Vector2(1920, 1080);

    public static Vector2 Position
    {
        get
        {
            var cam = Camera.main;
            Vector2 pos = cam.ScreenToViewportPoint(Input.mousePosition);

            pos -= Vector2.one / 2;
            pos.x *= resolution.x;
            pos.y *= resolution.y;

            return pos;
        }
    }

    public static Vector2 WordPosition
    {
        get
        {
            var pos = Mouse.current.position.ReadValue();
            pos = Camera.main.ScreenToWorldPoint(pos);

            return pos;
        }
    }


    public static bool FindUnder<T>(PointerEventData eventData, out T result)
    {
        result = default;
        var hits = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventData, hits);

        foreach (var hit in hits)
        {
            if(hit.gameObject.TryGetComponent<T>(out var t))
            {
                result = t;
                return true;
            }
        }

        return false;
    }

    public static bool FindUnder<T>(PointerEventData eventData, GameObject caster, out T result)
    {
        result = default;
        var hits = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventData, hits);

        foreach (var hit in hits)
        {
            if (hit.gameObject == caster) continue;

            if (hit.gameObject.TryGetComponent<T>(out var t))
            {
                result = t;
                return true;
            }
        }

        return false;
    }

    public static bool FindUnderInWorld<T>(out T result, GameObject caster = null)
    {
        result = default;

        
        var hits = Physics2D.RaycastAll(WordPosition, Vector2.zero, float.PositiveInfinity);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject == caster) continue;

            if (hit.collider.gameObject.TryGetComponent<T>(out var t))
            {
                result = t;
                return true;
            }
        }

        return false;
    }

    public static bool FindAllUnderInWorld<T>(out List<T> list, GameObject caster = null)
    {
        var hits = Physics2D.RaycastAll(WordPosition, Vector2.zero, float.PositiveInfinity);
        list = new List<T>();

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject == caster) continue;

            if (hit.collider.gameObject.TryGetComponent<T>(out var t))
            {
                list.Add(t);
            }
        }

        if (list.Count > 0) return true;
        return false;
    }
}
