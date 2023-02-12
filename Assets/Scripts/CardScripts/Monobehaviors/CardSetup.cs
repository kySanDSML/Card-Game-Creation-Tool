using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using Unity.UI;
public class CardSetup : MonoBehaviour
{
    [SerializeField] ScriptableCard CardData;
    [SerializeField] string CardName;
    [SerializeField] string CardText;
    [SerializeField] int CardHealth = 0;
    [SerializeField] int CardDamage = 0;
    [SerializeField] int CardCost = 0;
    [SerializeField] List<SummonType> summonType = new List<SummonType>();
    [SerializeField] CastType castType;

    [SerializeField] TextMeshProUGUI NameBanner;
    [SerializeField] TextMeshProUGUI CardDescription;
    [SerializeField] TextMeshProUGUI HealthBanner;
    [SerializeField] TextMeshProUGUI DamageBanner;
    [SerializeField] TextMeshProUGUI CostBanner;
    [SerializeField] TextMeshProUGUI TypeBanner;

    void Awake()
    {
        if(CardData != null)
        {
            if (CardData.GetType() == typeof(ScriptableSummon))
            {
                SetUpSummon();
            }
            else if (CardData.GetType() == typeof(ScriptableCast))
            {
                SetUpCastable();
            }
            else
            {
                CardName = "invalid card";
                CardText = "Bad card type";
            }
        }
    }

    void SetUpSummon()
    {
        if(CardData.GetType() == typeof(ScriptableSummon))
        {
            ScriptableSummon summon = (ScriptableSummon)CardData;
            CardHealth = summon.health;
            CardDamage = summon.damage;
            CardCost = summon.cost;
            CardName = summon.CardName;
            summonType = summon.CardType;
            CardText = "";
            foreach (keywords word in summon.CardKeywords)
            {
                CardText += "<align=\"center\"><b>" + word.ToString() + "</b></align>\n";
            }
            if (summon.Battlecry.action != Action.None)
            {
                string ActionText = "<b>Battlecry:</b> Do " + (summon.Battlecry.actionValue > 0 ? summon.Battlecry.actionValue +" ": "") + ObjectNames.NicifyVariableName(summon.Battlecry.action.ToString()) + " to" + (summon.Battlecry.targetCount > 0 ? summon.Battlecry.targetCount : "") + " " + ObjectNames.NicifyVariableName(summon.Battlecry.target.ToString());
                CardText += ActionText+"\n";
            }
            if (summon.Deathrattle.action != Action.None)
            {
                string ActionText = "<b>Deathrattle:</b> Do " + (summon.Deathrattle.actionValue > 0 ? summon.Deathrattle.actionValue+ " ": "") + ObjectNames.NicifyVariableName(summon.Deathrattle.action.ToString()) + " to" + summon.Deathrattle.targetCount + " " + ObjectNames.NicifyVariableName(summon.Deathrattle.target.ToString());
                CardText += ActionText + "\n";
            }

            NameBanner.text = CardName;
            CardDescription.text = CardText;
            HealthBanner.text = CardHealth.ToString();
            DamageBanner.text = CardDamage.ToString();
            CostBanner.text = CardCost.ToString();
            TypeBanner.text = summonType[0].ToString();
        }
        //Debug.Log(CardText);
    }

    void SetUpCastable()
    {
        if (CardData.GetType() == typeof(ScriptableCast))
        {
            ScriptableCast castable = (ScriptableCast)CardData;
            CardCost = castable.cost;
            CardName = castable.CardName;
            castType = (castable.CardType);
            CardText = "";
            foreach (ActionTargetPair action in castable.actions)
            {
                string ActionText = "Do " + (action.actionValue > 0 ? action.actionValue + " " : "") + ObjectNames.NicifyVariableName(action.action.ToString()) + " to" + (action.targetCount > 0 ? action.targetCount : "") + " " + ObjectNames.NicifyVariableName(action.target.ToString());
                CardText += ActionText + "\n";
            }

            NameBanner.text = CardName;
            CardDescription.text = CardText;
            CostBanner.text = CardCost.ToString();
            TypeBanner.text = castType.ToString();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(CardData != null)
        {
            NameBanner.text = CardName;
            CardDescription.text = CardText;
            if (CardData.GetType() == typeof(ScriptableSummon))
            {
                HealthBanner.text = CardHealth.ToString();
                DamageBanner.text = CardDamage.ToString();
                TypeBanner.text = summonType[0].ToString();
            }
            else if (CardData.GetType() == typeof(ScriptableCast))
            {
                TypeBanner.text = castType.ToString();
            }
            CostBanner.text = CardCost.ToString();
        }

    }
}