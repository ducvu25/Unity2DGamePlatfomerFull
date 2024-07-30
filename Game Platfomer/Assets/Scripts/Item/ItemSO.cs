using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment
}
[CreateAssetMenu(fileName ="Item", menuName ="Data/Item")]
public class ItemSO : ScriptableObject
{
    public int _id;
    public ItemType _itemType;
    public string _name;
    public Sprite _img;
}
