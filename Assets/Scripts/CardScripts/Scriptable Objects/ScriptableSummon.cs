using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Summon", menuName = "Card/Summon")]
public class ScriptableSummon : ScriptableCard
{
    public string CardName;
    public int health;
    public int damage;
    public int cost;

    public Team team;
    public bool canAttack;

    public List<keywords> CardKeywords = new List<keywords>();
    public List<SummonType> CardType = new List<SummonType>();
    public List<ActionTargetPair> EndOfTurnEffects = new List<ActionTargetPair>();
    public List<ActionTargetPair> StartOfTurnEffects = new List<ActionTargetPair>();
    public ActionTargetPair Battlecry = new ActionTargetPair();
    public ActionTargetPair Deathrattle = new ActionTargetPair();
}
