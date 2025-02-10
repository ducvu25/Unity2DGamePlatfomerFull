using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiShowInforPlayer : MonoBehaviour
{
    [Header("--- Major Stats -----")]
    [SerializeField] TextMeshProUGUI txtStrength;
    [SerializeField] TextMeshProUGUI txtAgility;
    [SerializeField] TextMeshProUGUI txtIntel;
    [SerializeField] TextMeshProUGUI txtVitali;

    [Header("\n---- Offensive ----")]
    [SerializeField] TextMeshProUGUI txtDame;
    [SerializeField] TextMeshProUGUI txtCritChange;
    [SerializeField] TextMeshProUGUI txtCritPower;
    [SerializeField] TextMeshProUGUI txtFireDame;
    [SerializeField] TextMeshProUGUI txtIceDame;
    [SerializeField] TextMeshProUGUI txtLightDame;

    [Header("\n---- Defensive ----")]
    [SerializeField] TextMeshProUGUI txtHealth;
    [SerializeField] TextMeshProUGUI txtArmor;
    [SerializeField] TextMeshProUGUI txtEvesion;
    [SerializeField] TextMeshProUGUI txtMagicRes;

    [Header("\n----- Equipemnt -----")]
    [SerializeField] BtnEquiment[] btnsShowEquentment;

    void OnEnable()
    {
        PlayerStats player = PlayerManager.instance.player.stats;
        // Major Stats
        txtStrength.text = player.strength.GetValue().ToString();
        txtAgility.text = player.agility.GetValue().ToString();
        txtIntel.text = player.intelligence.GetValue().ToString();
        txtVitali.text = player.vitality.GetValue().ToString();

        // Offensive Stats
        txtDame.text = player.damage.GetValue().ToString();
        txtCritChange.text = player.critChance.GetValue().ToString() + "%";
        txtCritPower.text = player.critPower.GetValue().ToString() + "%";
        txtFireDame.text = player.fireDamage.GetValue().ToString();
        txtIceDame.text = player.iceDamage.GetValue().ToString();
        txtLightDame.text = player.lightningDamage.GetValue().ToString();

        // Defensive Stats
        txtHealth.text = player.maxHealth.GetValue().ToString();
        txtArmor.text = player.armor.GetValue().ToString();
        txtEvesion.text = player.evasion.GetValue().ToString();
        txtMagicRes.text = player.magicResistance.GetValue().ToString();

        for (int i = 0; i < player.itemData.Length; i++)
        {
            btnsShowEquentment[i].ShowItem(player.itemData[i]);
        }
        
    }

}
