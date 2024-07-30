using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}
[CreateAssetMenu(fileName = "Item", menuName = "Data/Item_Equipment")]
public class ItemData_Equipment : ItemSO
{
    public EquipmentType _equipmentType;
}
