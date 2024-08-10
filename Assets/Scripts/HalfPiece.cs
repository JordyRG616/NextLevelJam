using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class HalfPiece : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GlobalObject currentSelectedDomino;
    [SerializeField] private ScriptableSignal OnCurrenteDominoChanged;
    [SerializeField] private ScriptableSignal OnRotatePressed;

    private List<Vector3> rotationPoints = new List<Vector3>();
    private int rotationIndex;
    public int value;
    public int previewDisabled;


    private void Start()
    {
        OnCurrenteDominoChanged.Register<GameObject>(CheckDisable);
    }

    private void CheckDisable(GameObject obj)
    {
        if (obj == null) gameObject.SetActive(false);
    }

    public void SetRotating(bool rotating, Transform pivot = null)
    {
        if (rotating)
        {
            rotationPoints.Clear();
            rotationPoints.Add(transform.parent.position);
            rotationPoints.Add(pivot.position + Vector3.right);
            rotationPoints.Add(pivot.position + Vector3.left);

            OnRotatePressed.Register(Rotate);
        } else
        {
            OnRotatePressed.Delist(Rotate);
        }
    }

    private void Rotate()
    {
        rotationIndex++;
        if (rotationIndex >= rotationPoints.Count) rotationIndex = 0;

        transform.parent.position = rotationPoints[rotationIndex];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(currentSelectedDomino.Reference != null)
        {
            currentSelectedDomino.SetActive(true);

            var domino = currentSelectedDomino.GetComponent<Domino>();
            domino.ConnectTo(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentSelectedDomino.Reference != null)
        {
            currentSelectedDomino.SetActive(false);
        }
    }
}
