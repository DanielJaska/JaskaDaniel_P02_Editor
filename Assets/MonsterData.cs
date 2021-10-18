using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MonsterData : ScriptableObject
{
    public enum Element {
        FIRE,
        WATER,
        EARTH,
        AIR,
        LIGHT,
        DARK
    }

    [SerializeField] string monsterSpecies;
    public string getMonsterSpecies { get { return monsterSpecies; } }
    public Element element;
    public Element getElement { get { return element; } }

    //Base Stats
    [SerializeField] int strength = 0;
    public int getStrength { get { return strength; } }
    [SerializeField] int dexterety = 0;
    public int getDexterety { get { return dexterety; } }
    [SerializeField] int intellegence = 0;
    public int getIntellegence { get { return intellegence; } }
    [SerializeField] int wisdom = 0;
    public int getWisdom { get { return wisdom; } }
    [SerializeField] int agility = 0;
    public int getAgility { get { return agility; } }
    [SerializeField] int constitution = 0;
    public int getConstitution { get { return constitution; } }


}
