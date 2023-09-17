using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryStats : MonoBehaviour
{
    public TextMeshProUGUI MaxHealthText;
    public TextMeshProUGUI MaxEnergyText;
    public TextMeshProUGUI DamageText;
    public TextMeshProUGUI ArmorText;
    public TextMeshProUGUI EscChText;
    public TextMeshProUGUI LuckText;
    public TextMeshProUGUI CrtChext;

    public GameObject Player;

    void Update()
    {
        PlayerData playerData = Player.GetComponent<PlayerData>();

        MaxHealthText.text = $"Max Health: {playerData.maxHealth}";
        MaxEnergyText.text = $"Max Energy: {playerData.maxEnergy}";
        DamageText.text = $"Damage: {playerData.damage}";
        ArmorText.text = $"Armor: {playerData.armor}";
        EscChText.text = $"Escape Chance: {playerData.escapeChance * 100}%";
        LuckText.text = $"Luck: ";
        CrtChext.text = $"Critical Chance: {playerData.CriticalChance}%";
    }
}
