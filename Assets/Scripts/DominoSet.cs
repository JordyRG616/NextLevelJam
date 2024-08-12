using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoSet : MonoBehaviour
{
    [SerializeField] private ScriptableSignal OnDominoPlayed;
    [SerializeField] private ScriptableSignal OnGameOver;
    [SerializeField] private SFXPlayer drawSfx;
    [Space]
    [SerializeField] private Domino dominoModel;
    [SerializeField] private Transform hand;
    [SerializeField] private int handSize = 3;
    [SerializeField] private List<ScriptableEnum> icons;


    private IEnumerator Start()
    {
        CreateDeck();
        CreateDeck();

        for (int i = 0; i < handSize; i++)
        {
            Draw();
            yield return new WaitForSeconds(.2f);
        }

        OnDominoPlayed.Register(CheckDraw);
    }

    private void CheckDraw()
    {
        Draw();
    }

    private void Update()
    {
        if (transform.childCount == 0 && hand.childCount == 0)
        {
            OnGameOver.Fire();
        }
    }

    private void CreateDeck()
    {
        for (int i = 0; i < icons.Count; i++)
        {
            for (int j = i; j < icons.Count; j++)
            {
                if (i == j) continue;
                var domino = Instantiate(dominoModel, transform);
                domino.transform.localPosition = Vector3.zero;
                domino.ReceiveIcons(icons[i], icons[j]);
            }
        }
    }

    private void Draw()
    {
        if (transform.childCount == 0)
        {
            return;
        }
        var rdm = Random.Range(0, transform.childCount);
        var piece = transform.GetChild(rdm);
        piece.SetParent(hand);
        piece.localScale = Vector3.one;
        drawSfx.Play();
    }
}
