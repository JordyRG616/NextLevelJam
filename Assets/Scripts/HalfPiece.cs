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
    private bool registered;

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
            var pos = transform.parent.position - pivot.position;
            rotationPoints.Add(transform.parent.position);
            rotationPoints.Add(pivot.position + (Vector3)Vector2.Perpendicular(pos.normalized));
            rotationPoints.Add(pivot.position - (Vector3)Vector2.Perpendicular(pos.normalized));

            OnRotatePressed.Register(Rotate);
            registered = true;
        } else
        {
            DelistRotation();
        }
    }

    public void DelistRotation()
    {
        Debug.Log("Delisted Rotation");
        OnRotatePressed.Delist(Rotate);
        registered = false;
    }

    private void Rotate()
    {
        if (transform == null || !registered) return;
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
