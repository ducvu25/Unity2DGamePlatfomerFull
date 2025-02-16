using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    [SerializeField] List<BtnEquiment> btnsItem;
    [SerializeField] GameObject goPreBtnItem;
    [SerializeField] int bagSize;
    [SerializeField] Transform transContent;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bagSize; i++) {
            InitValue(i);
        }
    }
    void InitValue(int i)
    {
        GameObject go = Instantiate(goPreBtnItem, transContent);
        BtnEquiment btnEquiment = go.GetComponent<BtnEquiment>();
        if (i < PlayerManager.instance.bags.Count)
        {
            btnEquiment.ShowItem(PlayerManager.instance.bags[i]);
        }
        else
            btnEquiment.ShowItem(null);
    }
}
