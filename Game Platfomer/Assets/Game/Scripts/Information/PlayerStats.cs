using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    Player player;

    public ItemData_Equipment[] itemData = {null, null, null, null} ;
    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
    }

    protected override void Die()
    {
        base.Die();
        player.stateMachine.ChangeState(player.deahState);
    }

    protected override void Start()
    {
        base.Start();
        player = PlayerManager.instance.player;
        SetLv(level);
    }

    protected override void Update()
    {
        base.Update();
    }
    public void UseEuipment(ItemData_Equipment equip)
    {
        int index = (int)equip._equipmentType;
        if (itemData[index] != null)
        {
            itemData[index].Unequipped(this);
        }
        itemData[index] = equip;
        itemData[index].Equipped(this);
    }
}
