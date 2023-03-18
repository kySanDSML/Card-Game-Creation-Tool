using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Castable", menuName = "Card/Cast")]
public class ScriptableCast : ScriptableCard
{   
    public CastType CardType;
    public string CardName;
    public int cost;
    public bool needsTarget = true;
    public List<ActionTargetPair> actions = new List<ActionTargetPair>();
}
