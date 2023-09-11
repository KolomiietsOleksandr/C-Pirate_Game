using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public int level;
    public int health;
    public int damage;
    public int armor;
    public int maxHealth;
    public int energy;
    public int maxEnergy;
    public int luck;
    public int Hunger;
    public int Thirst;

    public int MaxAttempts;

    public int MaxHunger;
    public int MaxThirst;

    public int ThirstForStep;
    public int HungerForStep;

    public int EnergyForAttack;

    public int CriticalMultiplier;
    public int CriticalChance;

    public GameObject currentShipPrefab;
    public GameObject PlayerPrefab;

    public int money;
    public int experience;

    public float escapeChance;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI thirstText;

    public Slider experienceSlider;

    public RuntimeAnimatorController CurrentAnimator;
    public RuntimeAnimatorController[] animators;

    private float timerH = 0f;
    private float timerE = 0f;
    private float interval = 1f;

    [SerializeField] private int baseExperience = 150; // ��������� �������� ������
    private float experienceMultiplier = 1.15f; // ������� ����������� �������

    private Item equippedWeapon;
    private Item equippedClothes;
    private Item equippedArtifact;

    // ����� ��� ��������� ����������� ����� ������

    public void Start()
    {
        CurrentAnimator = animators[0];
    }

    public void Update()
    {
        CalculateLevel();
        levelText.text = "Level: " + level.ToString();
        healthText.text = health.ToString() + "/" + maxHealth.ToString();
        moneyText.text = money.ToString();
        energyText.text = energy.ToString() + "/" + maxEnergy.ToString();
        hungerText.text = Hunger.ToString() + "/" + MaxHunger.ToString();
        thirstText.text = Thirst.ToString() + "/" + MaxThirst.ToString();

        // ���������� ������� ����� ��� ���������� ���� ����� ����������� �������
        int nextLevelExperience = Mathf.RoundToInt(baseExperience * Mathf.Pow(experienceMultiplier, level));

        // ������������ �������� ������ �� ��������� �������-����
        experienceSlider.minValue = 0;
        experienceSlider.maxValue = nextLevelExperience;

        // ��������� �������� ����� � �������-���
        experienceSlider.value = experience;

        if (SceneManager.GetActiveScene().name == "Pier")
        {
            if (health < maxHealth)
            {
                timerH += Time.deltaTime;

                if (timerH >= interval)
                {
                    health += 1;
                    timerH = 0f;
                }
            }
            if (energy < maxEnergy)
            {
                timerE += Time.deltaTime;

                if (timerE >= interval)
                {
                    energy += 1;
                    timerE = 0f;
                }
            }
        }

        if (health < 0)
        {
            health = 0;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (energy < 0)
        {
            energy = 0;
        }

        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }

        if (Hunger < 0)
        {
            Hunger = 0;
        }

        if (Hunger > MaxHunger)
        {
            Hunger = MaxHunger;
        }

        if (Thirst < 0)
        {
            Thirst = 0;
        }

        if (Thirst > MaxThirst)
        {
            Thirst = MaxThirst;
        }
    }

    private void CalculateLevel()
    {
        int totalExperience = Mathf.RoundToInt(baseExperience * Mathf.Pow(experienceMultiplier, level));

        // ����������� ����� �� ����� ���������� ������
        while (experience >= totalExperience)
        {
            level++;
            experience -= totalExperience;
            totalExperience = Mathf.RoundToInt(baseExperience * Mathf.Pow(experienceMultiplier, level));
        }
    }

    public void ApplyItemEffects(Item item)
    {
        switch (item.type)
        {
            case ItemType.Weapon:
                equippedWeapon = item;
                damage += item.AttackUp;
                CriticalChance += item.CrtUp;
                EnergyForAttack += item.EnFAtUPDOWN;
                break;

            case ItemType.Clothes:
                equippedClothes = item;
                armor += item.ArmorUp;
                maxHealth += item.HealthUp;
                maxEnergy += item.EnergyUp;
                break;

            case ItemType.Artifact:
                equippedArtifact = item;
                damage += item.AttackUp;
                CriticalChance += item.CrtUp;
                EnergyForAttack += item.EnFAtUPDOWN;
                armor += item.ArmorUp;
                maxHealth += item.HealthUp;
                maxEnergy += item.EnergyUp;
                break;
        }
    }

    public void RemoveItemEffects(Item item)
    {
        switch (item.type)
        {
            case ItemType.Weapon:
                if (equippedWeapon == item)
                {
                    equippedWeapon = null;
                    damage -= item.AttackUp;
                    CriticalChance -= item.CrtUp;
                    EnergyForAttack -= item.EnFAtUPDOWN;
                }
                break;

            case ItemType.Clothes:
                if (equippedClothes == item)
                {
                    equippedClothes = null;
                    armor -= item.ArmorUp;
                    maxHealth -= item.HealthUp;
                    maxEnergy -= item.EnergyUp;
                }
                break;

            case ItemType.Artifact:
                if (equippedArtifact == item)
                {
                    equippedArtifact = null;
                    damage -= item.AttackUp;
                    CriticalChance -= item.CrtUp;
                    EnergyForAttack -= item.EnFAtUPDOWN;
                    armor -= item.ArmorUp;
                    maxHealth -= item.HealthUp;
                    maxEnergy -= item.EnergyUp;
                }
                break;
        }
    }
}
