
[System.Serializable]
public class InventoryItem { 
    public ItemSO itemSO { get; private set; }
    public int stackSize { get; private set; }

    public InventoryItem(ItemSO item)
    {
        itemSO = item;
        stackSize = 1;
    }
    public void AddStack()=> stackSize++;
    public void RemoveStack()=> stackSize--;
}
