using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UI_ItemSlot : MonoBehaviour
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
        txtNumber.text = item.stackSize.ToString();
    }
}
