using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Fracta/Data/Global Object")]
public class GlobalObject : ScriptableObject
{
    [SerializeField] private bool observable;
    [ShowIf("observable"), SerializeField] private ScriptableSignal OnReferenceChanged;

    private GameObject _reference;
    public GameObject Reference
    {
        get => _reference;
        set
        {
            _reference = value;
            if (OnReferenceChanged != null) OnReferenceChanged.Fire(_reference);
        }
    }

    public Transform transform => Reference ? Reference.transform : null;

    private void OnValidate()
    {
        if (!observable) OnReferenceChanged = null;
    }

    public T GetComponent<T>()
    {
        return Reference.GetComponent<T>();
    }

    public void SetActive(bool active)
    {
        Reference.SetActive(active);
    }

    public void Clear()
    {
        Destroy(Reference);
        Reference = null;
    }
}
