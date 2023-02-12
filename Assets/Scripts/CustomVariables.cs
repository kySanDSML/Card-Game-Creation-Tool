using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    Damage,
    Heal,
}

public enum Target
{
    Any,
    All,
    Player,
    EnemyPlayer,
    AllMinion,
    AnyMinion,
    PlayerMinion,
    EnemyMinion
}

[System.Serializable]
public class ActionTargetPair
{
    public Action action;
    public int actionValue;
    public Target target;
    public int targetCount;
}

public enum SummonType
{
    Normal,
    Beast,
    Dragon
}

public enum keywords
{//stealing a bunch of random keywords for display only.
    Taunt,
    Rush,
    Windfury,
    Lifesteal,
    Overcharge,
    None = -1
}

public enum CastType
{
    Spell
}

