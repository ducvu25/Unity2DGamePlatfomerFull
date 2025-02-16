using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Type_Show_Item
{
    Infor,
    Bag,
    Shop
}
public class BtnEquiment : MonoBehaviour
{
    [SerializeField] Image imgShow;
    [SerializeField] UiShowLevelOfStrength uiShowLevelOfStrength;
    [SerializeField] ItemSO item;
    Type_Show_Item typeShowItem;
    public void ShowItem(ItemSO _item, Type_Show_Item _typeShowItem)
    {
        typeShowItem = _typeShowItem;
        item = _item;
        if(item == null)
        {
            imgShow.gameObject.SetActive(false);
            uiShowLevelOfStrength.gameObject.SetActive(false);
            return;
        }
        imgShow.gameObject.SetActive(true);
        imgShow.sprite = item._img;
        if(item is ItemData_Equipment && ((ItemData_Equipment)item)._level > 0)
        {
            uiShowLevelOfStrength.gameObject.SetActive(true);
            uiShowLevelOfStrength.SetColor(((ItemData_Equipment)item)._level);
        }
        else
        {
            uiShowLevelOfStrength.gameObject.SetActive(false);
        }
    }
    //public void HideItem()
    //{
    //    transform.GetComponent<Button>().onClick.RemoveAllListeners();
    //    uiShowLevelOfStrength.gameObject.SetActive(false);
    //    imgShow.gameObject.SetActive(false);
    //}
    bool[,] valueShowTypeItems = { { true, false }, { false, false }, { false, true } };
    public void ShowItem()
    {
        if (item != null)
        {
            GameManager.instance.uiManager.ShowItem(transform.position, item, valueShowTypeItems[(int)typeShowItem, 0], valueShowTypeItems[(int)typeShowItem, 1]);
        }
    }
}
