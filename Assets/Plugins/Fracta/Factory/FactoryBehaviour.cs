using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FactoryBehaviour : MonoBehaviour
{
    public Signal<IBlueprint> OnObjectCreated;

    [SerializeField] protected GameObject blueprint;
    protected FactoryHandler handler;
    protected FactoryPreset defaultConfiguration;

    protected List<GameObject> inactiveObjects = new List<GameObject>();


    protected virtual void Awake()
    {
        FractaMaster.RegisterFactory(this);
    }

    public virtual void InitializeFactory<T>() where T : IBlueprint
    {
        var iBlueprint = blueprint.GetComponent<IBlueprint>();
        handler = new FactoryHandler<T>(iBlueprint);
    }

    public virtual GameObject Create<T>(out T blueprint) where T : IBlueprint
    {
        var obj = GetConcrete();
        blueprint = obj.GetComponent<T>();
        blueprint.Configure(defaultConfiguration);

        OnObjectCreated.Fire(blueprint);

        return obj;
    }

    public virtual GameObject Create<T>() where T : IBlueprint
    {
        var obj = GetConcrete();
        var blueprint = obj.GetComponent<T>();
        blueprint.Configure(defaultConfiguration);

        OnObjectCreated.Fire(blueprint);

        return obj;
    }

    public virtual GameObject Create<T>(out T blueprint, FactoryPreset configuration) where T : IBlueprint
    {
        var obj = GetConcrete();
        blueprint = obj.GetComponent<T>();
        blueprint.Configure(configuration);

        OnObjectCreated.Fire(blueprint);

        return obj;
    }

    public virtual GameObject Create<T>(FactoryPreset configuration) where T : IBlueprint
    {
        var obj = GetConcrete();
        var blueprint = obj.GetComponent<T>();
        blueprint.Configure(configuration);

        OnObjectCreated.Fire(blueprint);

        return obj;
    }

    private GameObject GetConcrete()
    {
        if (inactiveObjects.Count > 0)
        {
            var obj = inactiveObjects[0];
            inactiveObjects.Remove(obj);
            return obj;
        } else
        {
            return Instantiate(handler.Print());
        }
    }

    private void OnValidate()
    {
        if (blueprint != null)
        {
            if (!blueprint.TryGetComponent<IBlueprint>(out var _b))
            {
                Debug.LogError("The assigned Game Object does not have a component that implements IBlueprint");
                blueprint = null;
            }
        }
    }

    private void OnDestroy()
    {
        FractaMaster.RemoveFactory(this);
    }
}

public abstract class FactoryHandler
{
    public abstract GameObject Print();
}

public class FactoryHandler<T> : FactoryHandler where T : IBlueprint
{
    private IBlueprint blueprint;


    public FactoryHandler(IBlueprint blueprint)
    {
        this.blueprint = blueprint;
    }

    public override GameObject Print()
    {
        return blueprint.GetConcrete();
    }
}

public class FactoryPreset
{

}
