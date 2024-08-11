using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    [SerializeField] private ScriptableSignal OnGameWon;
    [SerializeField] private List<Sprite> cityStages;
    [SerializeField] private SpriteRenderer cityRenderer;
    
    private List<BuildingType> typesInCity = new List<BuildingType>();
    private int cityLevel;

    public void ReceiveBuilding(BuildingType connectedBuilding)
    {
        if (connectedBuilding != BuildingType.None && !typesInCity.Contains(connectedBuilding))
        {
            typesInCity.Add(connectedBuilding);
            cityLevel++;
            cityRenderer.sprite = cityStages[cityLevel];

            if(cityLevel == 4)
            {
                OnGameWon.Fire();
            }
        }
    }
}
