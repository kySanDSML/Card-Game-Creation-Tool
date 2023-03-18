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
    AnyTarget,
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
    Readied,
    Doublestrike,
    Lifesteal,
    Empower,
    Testing,
    None = -1
}

public enum CastType
{
    Spell,
    Trap,
    Quest
}

public enum EnergyRegenType
{
    NoRegen,
    Incremental,
    MaxEnergy
}

[System.Serializable]
public class CastTargetPair
{
    public ScriptableCast Cast;
    public Target target;
}

[System.Serializable]
public class KeywordStringPair
{
    public keywords KeyWord;
    public string KeyWordAlias;

    public KeywordStringPair(keywords keyword, string alias)
    {
        this.KeyWord = keyword;
        this.KeyWordAlias = alias;
    }
}

[System.Serializable]
public class WordStringPair //this is basically like KeywordStringPair but for non enumerable keywords that can be added to the database whenever.
{
    public string word;
    public string alias;

    public WordStringPair(string word, string alias)
    {
        this.word = word;
        this.alias = alias;
    }
}

[System.Serializable]
public class NamedAction
{
    public string actionName;
    public ActionTargetPair pair;

    public NamedAction(string actionName, ActionTargetPair pair)
    {
        this.actionName = actionName;
        this.pair = pair;
    }

    public NamedAction(string actionName)
    {
        this.actionName = actionName;
        this.pair = new ActionTargetPair();
    }
}


