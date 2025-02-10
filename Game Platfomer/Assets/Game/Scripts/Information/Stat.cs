using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    [SerializeField] int baseValue;
    [SerializeField] List<int> buffs = new List<int>();

    public void AddBuff(int value)
    {
        buffs.Add(value);
    }
    public void RemoveBuff(int value)
    {
        buffs.Remove(value);
    }
    public int GetValue()
    {
        int sum = baseValue;
        for(int i = 0; buffs != null && i < buffs.Count; i++)
            sum += buffs[i];
        return sum; 
    }
    public void SetDefaulValue(int value)
    {
        baseValue = value;
    }
    public int GetDefaulValue() {
        return baseValue;
    }
}
