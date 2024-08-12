using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Domino : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [field:SerializeField] public ScriptableSignal OnDominoSelected { get; private set; }
    [SerializeField] private ScriptableSignal OnConfirmPressed;
    [SerializeField] private ScriptableSignal OnDominoPlayed;
    [SerializeField] private GlobalObject currentSelectedDomino;
    [field:SerializeField] public List<DominoValue> values { get; private set; }
    [Space]
    [SerializeField] private bool placed;
    [SerializeField] private GameObject outline;
    [SerializeField] private SFXPlayer placedSfx;
    [SerializeField] private SFXPlayer hoverSfx;

    public BuildingType connectedBuilding { get; private set; } = BuildingType.None;
    public bool connected;

    private bool selected;
    private bool hovered;

    private HalfPiece half;
    private HalfPiece otherHalf;
    private HalfPiece target;

    private void Start()
    {
        OnDominoSelected.CreateCustomSignal<Domino>();
        OnDominoSelected.Register<Domino>(CheckSelectedDomino);
    }

    public void ReceiveIcons(ScriptableEnum iconA, ScriptableEnum iconB)
    {
        values[0].Setup(iconA);
        values[1].Setup(iconB);
    }

    public void ConnectTo(HalfPiece target)
    {
        var half = values.Find(x => x.value == target.value).half;
        var parent = half.transform.parent;
        parent.position = target.transform.position;

        var otherHalf = values.Find(x => x.value != target.value).half;
        var direction = parent.position - target.transform.parent.position;
        otherHalf.transform.parent.position = parent.position + direction.normalized;

        this.half = half;//.GetComponent<HalfPiece>();
        this.otherHalf = otherHalf;//.GetComponent<HalfPiece>();
        this.target = target;

        this.half.SetRotating(false);
        this.otherHalf.SetRotating(true, parent);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (placed) return;

        selected = !selected;
        outline.SetActive(selected);

        if (selected) SetSelected();
        else
        {
            OnConfirmPressed.Delist(PlaceInstance);
            currentSelectedDomino.GetComponent<Domino>().values.ForEach(x => x.half.DelistRotation());
            currentSelectedDomino.Clear();
        }
    }

    private void SetSelected()
    {
        var newObj = Instantiate(gameObject);
        newObj.transform.localScale = Vector3.one;
        newObj.SetActive(false);
        newObj.GetComponent<Domino>().SetColliderActive(false);
        currentSelectedDomino.Reference = newObj;

        OnDominoSelected.Fire(this);
        OnConfirmPressed.Register(PlaceInstance);
    }

    private void PlaceInstance()
    {
        if (hovered || !selected) return;

        if (currentSelectedDomino.Reference.activeSelf == false) return;

        var domino = currentSelectedDomino.GetComponent<Domino>();
        domino.Place();
        placedSfx.Play();

        OnConfirmPressed.Delist(PlaceInstance);
        OnDominoSelected.Delist<Domino>(CheckSelectedDomino);
        currentSelectedDomino.Reference = null;
        Destroy(gameObject);
    }

    private void Place()
    {
        SetColliderActive(false);
        placed = true;
        outline.SetActive(false);

        var holder = values.Find(x => x.half == half.gameObject);
        values.Remove(holder);

        var targetDomino = target.GetComponentInParent<Domino>();
        if (targetDomino != null)
        {
            var holder_target = targetDomino.values.Find(x => x.half == target.gameObject);
            targetDomino.values.Remove(holder_target);
            connectedBuilding = targetDomino.connectedBuilding;
            targetDomino.connected = true;
        }
        else
        {
            var targetBuilding = target.GetComponentInParent<Building>();
            targetBuilding.Occupy();
            connectedBuilding = targetBuilding.type;
        }

        var direction = otherHalf.transform.parent.position - half.transform.parent.position;
        otherHalf.transform.position = otherHalf.transform.parent.position + direction.normalized;
        otherHalf.SetRotating(false);

        var hit = Physics2D.OverlapCircle(otherHalf.transform.parent.position, .2f);
        if (hit != null)
        {
            if (hit.TryGetComponent<City>(out var city))
            {
                city.ReceiveBuilding(connectedBuilding);
            }
        }

        Invoke("FireDominoPlaced", .15f);
    }

    private void FireDominoPlaced()
    {
        OnDominoPlayed.Fire();
    }

    public void SimplePlace()
    {
        SetColliderActive(false);
        placed = true;
        outline.SetActive(false);
    }

    private void CheckSelectedDomino(Domino selectedDomino)
    {
        if (!placed || connected) return;

        foreach (var value in selectedDomino.values)
        {
            foreach(var selfValue in values)
            {
                if(selfValue.value == value.value)
                {
                    selfValue.half.gameObject.SetActive(true);
                }
            }
        }
    }

    public void SetColliderActive(bool active)
    {
        GetComponent<Collider2D>().enabled = active;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (placed) return;

        hovered = true;
        transform.localScale = Vector3.one * 1.1f;
        hoverSfx.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (placed) return;

        hovered = false;
        if (!selected) transform.localScale = Vector3.one;
    }
}

[System.Serializable]
public class DominoValue
{
    public int value;
    public SpriteRenderer icon;
    public HalfPiece half;

    public void Setup(ScriptableEnum source)
    {
        value = source.Value;
        icon.sprite = source.Icon;
        half.value = value;
    }
}