using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<InventoryItem> items = new List<InventoryItem>();
    Dictionary<ItemSO, InventoryItem> inventoryItems = new Dictionary<ItemSO, InventoryItem>();

    [SerializeField] Transform goParentBag;
    //[SerializeField] GameObject goPreItemBag;

    UI_ItemSlot[] ui_ItemSlots;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        ui_ItemSlots = goParentBag.GetComponentsInChildren<UI_ItemSlot>();
    }
    public void Add(ItemSO item)
    {
        if(inventoryItems.TryGetValue(item, out InventoryItem inventoryItem))
        {
            inventoryItem.AddStack();
        }
        else
        {
            InventoryItem i = new InventoryItem(item);
            items.Add(i);
            inventoryItems.Add(item, i);
        }
        UpdateItemSlot();
    }
    public void Remove(ItemSO item) {
        if (inventoryItems.TryGetValue(item, out InventoryItem inventoryItem))
        {
            inventoryItem.RemoveStack();
            if(inventoryItem.stackSize == 0)
            {
                items.Remove(inventoryItem);
                inventoryItems.Remove(item);    
            }
        }
        UpdateItemSlot();
    }
    void UpdateItemSlot()
    {
        for(int i=0; i< items.Count; i++)
        {
            ui_ItemSlots[i].SetValue(items[i]);
        }
    }
}
