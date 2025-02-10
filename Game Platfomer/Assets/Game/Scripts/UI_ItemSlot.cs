using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    InventoryItem item;
    [SerializeField] Image imgShow;
    [SerializeField] TextMeshProUGUI txtNumber;
    private void Start()
    {
        Color a = Color.white;
        a.a = 0;
        imgShow.color = a;
        txtNumber.text = "";
    }
    public void SetValue(InventoryItem _item)
    {
        item = _item;
        imgShow.color = Color.white;
        imgShow.sprite = item.itemSO._img;
        txtNumber.text = item.itemSO is ItemData_Equipment ? "" : item.stackSize.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Click");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("UP");
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Debug.Log("move");
    }
}
