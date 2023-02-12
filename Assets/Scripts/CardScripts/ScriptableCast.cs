using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Castable", menuName = "Card/Cast")]
public class ScriptableCast : ScriptableCard
{   
    public CastType CardType;
    public List<ActionTargetPair> actions = new List<ActionTargetPair>();

    
}
