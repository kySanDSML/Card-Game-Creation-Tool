using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Summon", menuName = "Card/Summon")]
public class ScriptableSummon : ScriptableCard
{
    public List<keywords> CardKeywords = new List<keywords>();
    public List<SummonType> CardType = new List<SummonType>();

    public int health;
    public int damage;

    public Team team;
    public bool canAttack;
}
