using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fracta/Data/Enum")]
public class ScriptableEnum : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; protected set; }
    [field: SerializeField] public int Value { get; protected set; }
}
