using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    Damage,
    Heal,
    None = -1
}

public enum Target
{
    Any,
    Everything,
    AnyPlayer,
    Player,
    EnemyPlayer,
    AllMinions,
    AnyMinion,
    PlayerMinion,
    EnemyMinion,
    AllPlayerMinions,
    AllEnemyMinions
}

public enum Team
{
    Player_0,
    Player_1
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

[System.Serializable]
public class CastTargetPair
{
    public ScriptableCast Cast;
    public Target target;
}

