using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolShowItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtName;
    [SerializeField] TextMeshProUGUI txtPrice;
    [SerializeField] TextMeshProUGUI txtDes;
    [SerializeField] GameObject goIntensify;
    [SerializeField] TextMeshProUGUI txtIntensify;
    [SerializeField] GameObject btnUnequi;
    [SerializeField] GameObject btnEqui;
    [SerializeField] GameObject btnUse;
    [SerializeField] GameObject btnSell;
    [SerializeField] GameObject btnThrowAway;

    ItemSO item;
    public void ShowItem(ItemSO _item, bool isEqui = false, bool isShop = false)
    {
        item = _item;
        // kiểm tra vị trí chuột và hiển thị thông tin tại vị trí chuột
        btnUnequi.SetActive(false);
        btnSell.SetActive(false);
        btnThrowAway.SetActive(false);
        btnUse.SetActive(false);
        btnEqui.SetActive(false);

        txtPrice.text = "" + _item._price;
        txtDes.text =" " + _item._description;

        if (_item is ItemData_Equipment)
        {
            ItemData_Equipment i = (ItemData_Equipment)_item;

            
            if (i._level > 0)
                txtName.text = i._name + " + " + i._level;
            else
                txtName.text = i._name;
            txtName.color = GameValue.Instance.colorsLevelOfStrength[i._level];
            goIntensify.SetActive(true);
            txtIntensify.gameObject.SetActive(true);
            txtIntensify.text = i.ToString();
            if (isShop)
            {
                btnSell.SetActive(true);
                return;
            }
            if (isEqui)
            {
                btnUnequi.SetActive (true);
                return;
            }
            btnEqui.SetActive (true);
        }
        else
        {
            txtName.text = _item._name;
            txtName.color = GameValue.Instance.colorsLevelOfStrength[0];
            goIntensify.SetActive(false);
            txtIntensify.gameObject.SetActive(false);
            if (isShop)
            {
                btnSell.SetActive(true);
                return;
            }
            if (_item.canUse)
            {
                btnUse.SetActive(true);
            }
        }
        btnThrowAway.SetActive(true);
    }
    public void SetBtnClick(int i)
    {
        switch (i)
        {
            case 0:
                {
                    //Debug.Log("Un");
                    transform.gameObject.SetActive(false);
                    GameManager.instance.uiManager.uiShowInforPlayer.Unequenment((item as ItemData_Equipment)._equipmentType);
                    break;
                }
            case 1:
                {
                    Debug.Log("Eq");
                    break;
                }
            case 2:
                {
                    Debug.Log("Us");
                    break;
                }
            case 3:
                {
                    Debug.Log("Thr");
                    break;
                }
            case 4:
                {
                    Debug.Log("Sel");
                    break;
                }
        }
    }
}
