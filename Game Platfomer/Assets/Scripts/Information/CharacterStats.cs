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
    public bool isShocked; // choang, giam 20% danh trung

    float ignitedTimer;
    float chilledTimer;
    float shockedTimer;

    float ignitedDamageCoodlown = 0.3f;
    float ignitedDamageTimer;

    int igniteDamage;

    public int currentHealth;
    public System.Action updateHP;

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue()*5;
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = GetMaxHealthValue();
        critPower.SetDefaulValue(150);
    }
    protected virtual void Update()
    {
        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;

        ignitedDamageTimer -= Time.deltaTime;

        if(ignitedTimer < 0)
            isIgnited = false;

        if(chilledTimer < 0)
            isChilled = false;

        if (shockedTimer < 0)
        {
            isShocked = false;
        }

        if (ignitedDamageTimer < 0 && isIgnited)
        {
            Debug.Log("Take burn damage");
            currentHealth -= igniteDamage;
            if (currentHealth <= 0)
            {
                Die();
            }
            ignitedDamageTimer = ignitedDamageCoodlown;
        }
    }
    bool CanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();
        if (isShocked) totalEvasion += 20;
        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }
        return false;
    } 
    int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        if (_targetStats.isChilled)
            totalDamage -= _targetStats.armor.GetValue() * 8 / 100;
        else
            totalDamage -= _targetStats.armor.GetValue();

         totalDamage = Mathf.Clamp(totalDamage, 1, int.MaxValue);
        return totalDamage;
    }
    public virtual void DoDamage(CharacterStats _targetStats)
    {
       if(CanAvoidAttack(_targetStats))
        {
            Debug.Log("Miss");
            return;
        }
        int totalDamage = GetDamage();
        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
        //_targetStats.TakeDamage(totalDamage);
        DoMagicalDamage(_targetStats);
    }
    public void SetIgniteDamage(int value) => igniteDamage = value;
    public virtual void TakeDamage(int value, bool magic = false)
    {
        if(updateHP != null) 
            updateHP();

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
    public virtual void DoMagicalDamage(CharacterStats _tagetStats)
    {
        int _fire = fireDamage.GetValue();
        int _ice = iceDamage.GetValue();
        int _light = lightingDamage.GetValue();

        int totalDame = _fire + _ice + _light + intelligence.GetValue();
        totalDame -= _tagetStats.magicResistance.GetValue() + (_tagetStats.intelligence.GetValue()*3);
        totalDame = Mathf.Clamp(totalDame, 1, int.MaxValue);
        _tagetStats.TakeDamage(totalDame);
        if (Mathf.Max(_fire, _ice, _light) <= 0) return;

        bool canApplyIgnie = _ice > _fire && _ice > _light;
        bool canApplyFire = _fire > _ice && _fire > _light;
        bool canApplyShock = _light > _fire && _light > _ice;

        if (canApplyFire)
            _tagetStats.SetIgniteDamage(_fire * 2/10);

        _tagetStats.ApplyAilments(canApplyIgnie, canApplyFire, canApplyShock);
    }
    public void ApplyAilments(bool _ignite, bool _chill, bool _shocked)
    {
        if(isIgnited || isChilled || isIgnited)
        {
            return;
        }
        if (_ignite)
        {
            isIgnited = _ignite;
            ignitedTimer = 2;
        }
        if (_chill)
        {
            isChilled = _chill;
            chilledTimer = 2;
        }
        if (_shocked)
        {
            isShocked = _shocked;
            shockedTimer = 2;
        }
    }
}
