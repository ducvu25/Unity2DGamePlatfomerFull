using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Major stats")]
    public Stat strength; // sức mạnh
    public Stat agility; // nhanh nhẹn
    public Stat intelligence; // trí tuệ
    public Stat vitality;

    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor; // giáp
    public Stat evasion; // né tránh
    public Stat magicResistance; // giáp phép

    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightingDamage;

    public bool isIgnited; // chay
    public bool isChilled; // dong bang
    public bool isShocked; // choang

    [SerializeField] private float ailmentsDuration = 4;
    private float ignitedTimer;
    private float chilledTimer;
    private float shockedTimer;

    [SerializeField] int currentHealth;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();
    }
    protected virtual void Update()
    {

    }
    bool CanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();
        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }
        return false;
    } 
    int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        totalDamage = Mathf.Clamp(totalDamage - _targetStats.armor.GetValue(), 1, int.MaxValue);
        return totalDamage;
    }
    public virtual void DoDamage(CharacterStats _targetStats)
    {
       if(CanAvoidAttack(_targetStats))
        {
            Debug.Log("Miss");
            return;
        }
        int totalDamage = damage.GetValue() + strength.GetValue();
        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);
    }
    public virtual void TakeDamage(int value, bool magic = false)
    {
        if(Random.Range(0, 100) < evasion.GetValue() + agility.GetValue())
        {
            return;
        }
        if (!magic)
        {
            currentHealth -= Mathf.Clamp(value - armor.GetValue(), 1, 1000);
        }
        else
        {
            currentHealth -= Mathf.Clamp(value - magicResistance.GetValue(), 1, 1000);
        }
        
        //Debug.Log(hp);
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    
    protected virtual void Die()
    {

    }
    public float GetHp() { return currentHealth; }
    public virtual int GetDamage()
    {
        int sum = damage.GetValue() + strength.GetValue();
        if (CanCrit())
        {
            sum = sum * critPower.GetValue() / 100;
        }
        return sum;
    }
    bool CanCrit()
    {
        //Debug.Log(critChance.GetValue() + agility.GetValue());
        return Random.Range(0, 100) < (critChance.GetValue() + agility.GetValue());
    }
    public int GetMagicDamage()
    {
        int _fire = fireDamage.GetValue();
        int _ice = iceDamage.GetValue();
        int _light = lightingDamage.GetValue();

        int totalDame = _fire + _ice + _light + intelligence.GetValue();
        return totalDame;
    }
    public void ApplyAilments(bool _ignite, bool _chill, bool _shocked)
    {
        if(isIgnited || isChilled || isIgnited)
        {
            return;
        }
        isIgnited = _ignite;
        isChilled = _chill;
        isShocked = _shocked;
    }
}
