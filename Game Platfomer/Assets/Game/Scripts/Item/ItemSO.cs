using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
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
    public string _description;
    public Sprite _img;
    public int _price;
    public bool canUse;
    [Range(0, 100)]
    public float _ratio;
}
