using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoHalfBG : MonoBehaviour
{
    [SerializeField] private Transform bg;
    [SerializeField] private Ruler ruler;


    private void LateUpdate()
    {
        bg.up = ruler.direction;
    }
}
