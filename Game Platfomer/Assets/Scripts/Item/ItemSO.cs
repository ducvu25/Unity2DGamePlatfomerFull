using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName ="Data/Item")]
public class ItemSO : ScriptableObject
{
    public int _id;
    public string _name;
    public Sprite _img;
}
