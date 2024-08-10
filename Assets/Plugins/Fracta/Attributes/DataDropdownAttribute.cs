using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDropdownAttribute : PropertyAttribute
{
    public Type type;
    public string path = "Assets/Data";

    public DataDropdownAttribute(Type type)
    {
        this.type = type;
    }

    public DataDropdownAttribute(Type type, string path)
    {
        this.type = type;
        this.path = path;
    }
}
