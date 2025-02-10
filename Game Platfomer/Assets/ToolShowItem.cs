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

    public void ShowItem(ItemSO item, bool isEqui = false, bool isShop = false)
    {
        // kiểm tra vị trí chuột và hiển thị thông tin tại vị trí chuột
        btnUnequi.SetActive(false);
        btnSell.SetActive(false);
        btnThrowAway.SetActive(false);
        btnUse.SetActive(false);
        btnEqui.SetActive(false);

        txtPrice.text = "" + item._price;
        txtDes.text =" " + item._description;

        if (item is ItemData_Equipment)
        {
            ItemData_Equipment i = (ItemData_Equipment)item;

            
            if (i._level > 0)
                txtName.text = i._name + " + " + i._level;
            else
                txtName.text = i._name;

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
            txtName.text = item._name;
            
            goIntensify.SetActive(false);
            txtIntensify.gameObject.SetActive(false);
            if (isShop)
            {
                btnSell.SetActive(true);
                return;
            }
            if (item.canUse)
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
                    Debug.Log("Un");
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
