using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnEquiment : MonoBehaviour
{
    [SerializeField] Image imgShow;
    [SerializeField] UiShowLevelOfStrength uiShowLevelOfStrength;
    [SerializeField] ItemSO item;

    public void ShowItem(ItemSO _item)
    {
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
    public void HideItem()
    {
        transform.GetComponent<Button>().onClick.RemoveAllListeners();
        uiShowLevelOfStrength.gameObject.SetActive(false);
        imgShow.gameObject.SetActive(false);
    }
    public void ShowItem()
    {
        if (item != null)
        {
            GameManager.instance.uiManager.ShowItem(item, true);
        }
    }
}
