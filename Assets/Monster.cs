using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : ScriptableObject
{
    public MonsterData data;

    public int monsterLevel = 1;

    public int currentHealth = 0;
    [SerializeField] int maxHealth = 0;

    public int currentMana = 0;
    [SerializeField] int maxMana = 0;

    public int strength = 0;

    public int dexterity = 0;

    public void SetHP(int value)
    {
        maxHealth = value;
        currentHealth = maxHealth;
    }

    public void SetMana(int value)
    {
        maxMana = value;
        currentMana = maxMana;
    }

    public void SetupMonster(MonsterData newData, int level)
    {
        data = newData;
        monsterLevel = level;
        SetHP(CalculateVital(data.getConstitution));
        SetMana(CalculateVital(data.getWisdom));
        strength = CalculateStat(data.getStrength);
        dexterity = CalculateStat(data.getDexterety);
    }

    int CalculateVital(int baseStat)
    {
        int statValue = 0;

        statValue = baseStat * 10 * monsterLevel;

        if (statValue < 10)
        {
            statValue = 10;
        }

        return statValue;
    }

    int CalculateStat(int baseStat)
    {
        int statValue = 0;

        if (baseStat != 0)
        {
            statValue = baseStat * 2 * monsterLevel;
        }
        else
        {
            statValue = 1;
        }

        return statValue;
    }
}
