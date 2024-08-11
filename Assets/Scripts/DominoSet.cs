using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoSet : MonoBehaviour
{
    [SerializeField] private ScriptableSignal OnDominoPlayed;
    [Space]
    [SerializeField] private Domino dominoModel;
    [SerializeField] private Transform hand;
    [SerializeField] private int handSize = 3;
    [SerializeField] private List<ScriptableEnum> icons;


    private void Start()
    {
        CreateDeck();

        var rdm = Random.Range(0, transform.childCount);
        var first = transform.GetChild(rdm);
        first.SetParent(null);
        first.position = Vector3.zero;
        first.GetComponent<Domino>().SimplePlace();

        for (int i = 0; i < handSize; i++)
        {
            Draw();
        }

        OnDominoPlayed.Register(CheckDraw);
    }

    private void CheckDraw()
    {
        Draw();
    }

    private void CreateDeck()
    {
        for (int i = 0; i < icons.Count; i++)
        {
            for (int j = i; j < icons.Count; j++)
            {
                var domino = Instantiate(dominoModel, transform);
                domino.transform.localPosition = Vector3.zero;
                domino.ReceiveIcons(icons[i], icons[j]);
            }
        }
    }

    private Transform Draw()
    {
        var rdm = Random.Range(0, transform.childCount);
        var piece = transform.GetChild(rdm);
        piece.SetParent(hand);
        return piece;
    }
}
