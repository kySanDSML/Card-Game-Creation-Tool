using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using Unity.UI;
public class CardSetup : MonoBehaviour
{
    [SerializeField] public ScriptableCard CardData;
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
    [SerializeField] GameObject HealthBannerHold;
    [SerializeField] GameObject DamageBannerHold;
    [SerializeField] GameObject TypeBannerHold;

    [SerializeField] bool initialized = false;

    void Awake()
    {
        if(CardData != null && !initialized)
        {
            SetUp();
        }
        else
        {
            initialized = false;
        }
    }

    void SetUpSummon()
    {
        if(CardData.GetType() == typeof(ScriptableSummon))
        {
            if (HealthBannerHold != null)
            {
                HealthBannerHold.SetActive(true);
            }

            if (DamageBannerHold != null)
            {
                DamageBannerHold.SetActive(true);
            }

            if(TypeBannerHold != null)
            {
                TypeBannerHold.GetComponent<UnityEngine.UI.Image>().color = new Color32(170, 139, 221, 255);
            }
            ScriptableSummon summon = (ScriptableSummon)CardData;
            CardHealth = summon.health;
            CardDamage = summon.damage;
            CardCost = summon.cost;
            CardName = summon.CardName;
            summonType = summon.CardType;
            CardText = "";
            foreach (keywords word in summon.CardKeywords)
            {
                CardText += "<align=\"center\"><b>" + (KeywordAliases.getAlias(word.ToString())) + "</b></align>\n";
            }
            
            foreach(NamedAction action in summon.namedActions)
            {
                if (action.pair.action != Action.None)
                {
                    string ActionText = "<b>" + KeywordAliases.getWordAlias(action.actionName) + ":</b> Do " + (action.pair.actionValue > 0 ? action.pair.actionValue + " " : "") + ObjectNames.NicifyVariableName(action.pair.action.ToString()) + " to" + (action.pair.targetCount > 0 ? action.pair.targetCount : "") + " " + ObjectNames.NicifyVariableName(action.pair.target.ToString());
                    CardText += ActionText + "\n";
                }
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
            if (HealthBannerHold != null)
            {
                HealthBannerHold.SetActive(false);
            }

            if (DamageBannerHold != null)
            {
                DamageBannerHold.SetActive(false);
            }

            if (TypeBannerHold != null)
            {
                TypeBannerHold.GetComponent<UnityEngine.UI.Image>().color = new Color32(0, 229, 225, 255);
            }
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
                TypeBanner.text = summonType.Count > 0 ? summonType[0].ToString() : "None";
            }
            else if (CardData.GetType() == typeof(ScriptableCast))
            {
                TypeBanner.text = castType.ToString();
            }
            CostBanner.text = CardCost.ToString();
        }else if(initialized == false && CardData != null)
        {
            SetUp();
            initialized = true;
        }

    }

    public void SetUp()
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
}
