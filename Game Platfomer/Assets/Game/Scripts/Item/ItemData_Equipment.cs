using System;
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
    string buffColor = "<color=#FFA500>"; // Màu cam
    public int _level;
    [Range(0, 100)]
    public float _percentageModifier;
    public EquipmentType _equipmentType;
    [Header("Value stats")]
    public int _strength;
    public int _agility;
    public int _intelligence; // trí tuệ
    public int _vitality;

    public int _damage;
    public int _critChance;
    public int _critPower;

    public int _maxHealth;
    public int _armor; // giáp
    public int _evasion; // né tránh
    public int _magicResistance; // giáp phép

    public int _fireDamage;
    public int _iceDamage;
    public int _lightningDamage;

    public void Equipped(PlayerStats player)
    {
        player.strength.AddBuff(CalculateTotalBuff(_strength));
        player.agility.AddBuff(CalculateTotalBuff(_agility));
        player.intelligence.AddBuff(CalculateTotalBuff(_intelligence));
        player.vitality.AddBuff(CalculateTotalBuff(_vitality));

        player.damage.AddBuff(CalculateTotalBuff(_damage));
        player.critChance.AddBuff(CalculateTotalBuff(_critChance)); // Tính buff cho crit chance
        player.critPower.AddBuff(CalculateTotalBuff(_critPower)); // Tính buff cho crit power

        player.maxHealth.AddBuff(CalculateTotalBuff(_maxHealth));
        player.armor.AddBuff(CalculateTotalBuff(_armor)); // Tính buff cho armor
        player.evasion.AddBuff(CalculateTotalBuff(_evasion)); // Tính buff cho evasion
        player.magicResistance.AddBuff(CalculateTotalBuff(_magicResistance)); // Tính buff cho magic resistance

        player.fireDamage.AddBuff(CalculateTotalBuff(_fireDamage));
        player.iceDamage.AddBuff(CalculateTotalBuff(_iceDamage));
        player.lightningDamage.AddBuff(CalculateTotalBuff(_lightningDamage));
    }

    public void Unequipped(PlayerStats player)
    {
        player.strength.RemoveBuff(CalculateTotalBuff(_strength));
        player.agility.RemoveBuff(CalculateTotalBuff(_agility));
        player.intelligence.RemoveBuff(CalculateTotalBuff(_intelligence));
        player.vitality.RemoveBuff(CalculateTotalBuff(_vitality));

        player.damage.RemoveBuff(CalculateTotalBuff(_damage));
        player.critChance.RemoveBuff(CalculateTotalBuff(_critChance)); // Tính buff cho crit chance
        player.critPower.RemoveBuff(CalculateTotalBuff(_critPower)); // Tính buff cho crit power

        player.maxHealth.RemoveBuff(CalculateTotalBuff(_maxHealth));
        player.armor.RemoveBuff(CalculateTotalBuff(_armor)); // Tính buff cho armor
        player.evasion.RemoveBuff(CalculateTotalBuff(_evasion)); // Tính buff cho evasion
        player.magicResistance.RemoveBuff(CalculateTotalBuff(_magicResistance)); // Tính buff cho magic resistance

        player.fireDamage.RemoveBuff(CalculateTotalBuff(_fireDamage));
        player.iceDamage.RemoveBuff(CalculateTotalBuff(_iceDamage));
        player.lightningDamage.RemoveBuff(CalculateTotalBuff(_lightningDamage));
    }

    public override string ToString()
    {
        string result = "";

        // Mã màu cho giá trị buff thêm
        string buffColor = "<color=#FFA500>"; // Màu cam

        // Strength
        if (_strength > 0)
        {
            int strengthBuff = CalculateTotalBuff(_strength) - _strength;
            result += $" + {_strength} ({buffColor}+{strengthBuff}</color>) strength\n";
        }

        // Agility
        if (_agility > 0)
        {
            int agilityBuff = CalculateTotalBuff(_agility) - _agility;
            result += $" + {_agility} ({buffColor}+{agilityBuff}</color>) agility\n";
        }

        // Intelligence
        if (_intelligence > 0)
        {
            int intelligenceBuff = CalculateTotalBuff(_intelligence) - _intelligence;
            result += $" + {_intelligence} ({buffColor}+{intelligenceBuff}</color>) intelligence\n";
        }

        // Vitality
        if (_vitality > 0)
        {
            int vitalityBuff = CalculateTotalBuff(_vitality) - _vitality;
            result += $" + {_vitality} ({buffColor}+{vitalityBuff}</color>) vitality\n";
        }

        // Damage
        if (_damage > 0)
        {
            int damageBuff = CalculateTotalBuff(_damage) - _damage;
            result += $" + {_damage} ({buffColor}+{damageBuff}</color>) damage\n";
        }

        // Crit Chance
        if (_critChance > 0)
        {
            int critChanceBuff = CalculateTotalBuff(_critChance) - _critChance;
            result += $" + {_critChance}% ({buffColor}+{critChanceBuff}%</color>) crit chance\n";
        }

        // Crit Power
        if (_critPower > 0)
        {
            int critPowerBuff = CalculateTotalBuff(_critPower) - _critChance;
            result += $" + {_critPower} ({buffColor}+{critPowerBuff}</color>) crit power\n";
        }

        // Max Health
        if (_maxHealth > 0)
        {
            int maxHealthBuff = CalculateTotalBuff(_maxHealth) - _maxHealth;
            result += $" + {_maxHealth} ({buffColor}+{maxHealthBuff}</color>) max health\n";
        }

        // Armor
        if (_armor > 0)
        {
            int armorBuff = CalculateTotalBuff(_armor) - _armor;
            result += $" + {_armor} ({buffColor}+{armorBuff}</color>) armor\n";
        }

        // Evasion
        if (_evasion > 0)
        {
            int evasionBuff = CalculateTotalBuff(_evasion) - _evasion;
            result += $" + {_evasion} ({buffColor}+{evasionBuff}</color>) evasion\n";
        }

        // Magic Resistance
        if (_magicResistance > 0)
        {
            int magicResistanceBuff = CalculateTotalBuff(_magicResistance) - _magicResistance;
            result += $" + {_magicResistance} ({buffColor}+{magicResistanceBuff}</color>) magic resistance\n";
        }

        // Fire Damage
        if (_fireDamage > 0)
        {
            int fireDamageBuff = CalculateTotalBuff(_fireDamage) - _fireDamage;
            result += $" + {_fireDamage} ({buffColor}+{fireDamageBuff}</color>) fire damage\n";
        }

        // Ice Damage
        if (_iceDamage > 0)
        {
            int iceDamageBuff = CalculateTotalBuff(_iceDamage) - _iceDamage;
            result += $" + {_iceDamage} ({buffColor}+{iceDamageBuff}</color>) ice damage\n";
        }

        // Lightning Damage
        if (_lightningDamage > 0)
        {
            int lightningDamageBuff = CalculateTotalBuff(_lightningDamage) - _lightningDamage;
            result += $" + {_lightningDamage} ({buffColor}+{lightningDamageBuff}</color>) lightning damage\n";
        }

        return result;
    }
    private int CalculateTotalBuff(int baseValue)
    {
        return (int)(baseValue * Mathf.Pow(1f + _percentageModifier / 100, _level));
    }
}
