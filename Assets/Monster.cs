using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : ScriptableObject
{
    public MonsterData data;

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
}
