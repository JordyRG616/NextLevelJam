using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ManagerBehaviour : MonoBehaviour
{
    private List<IController> controllers;

    protected virtual void Awake()
    {
        FractaMaster.RegisterManager(this);

        controllers = GetComponents<IController>().ToList();
    }

    private void OnDestroy()
    {
        FractaMaster.RemoveManager(this);
    }

    public T GetController<T>() where T : IController
    {
        var controller = controllers.Find(c => c is T);

        if (controller != null) return (T)controller;
        else return default(T);
    }
}
