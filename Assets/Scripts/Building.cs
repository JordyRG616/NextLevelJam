using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private static List<ScriptableEnum> staticIcons;
    [SerializeField] private ScriptableSignal OnDominoSelected;
    [SerializeField] private HalfPiece halfPiece;
    [SerializeField] private SpriteRenderer icon;
    [Space]
    [SerializeField] private List<ScriptableEnum> icons;
    [field:SerializeField] public BuildingType type { get; private set; }


    private void Start()
    {
        if (staticIcons == null) staticIcons = new List<ScriptableEnum>(icons);

        var icon = staticIcons.GetRandomItem();
        staticIcons.Remove(icon);
        this.icon.sprite = icon.Icon;

        halfPiece.value = icon.Value;

        OnDominoSelected.Register<Domino>(CheckSelectedDomino);
    }

    private void CheckSelectedDomino(Domino domino)
    {
        foreach (var value in domino.values)
        {
            if (halfPiece.value == value.value)
            {
                halfPiece.gameObject.SetActive(true);
            }
        }
    }

    public void Occupy()
    {
        OnDominoSelected.Delist<Domino>(CheckSelectedDomino);
        halfPiece.gameObject.SetActive(false);
    }
}

public enum BuildingType
{
    None,
    Serraria,
    Lenhador,
    Mina,
    Ferreiro
}