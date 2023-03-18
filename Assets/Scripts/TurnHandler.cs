using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler: MonoBehaviour
{
    PlayerStats ps;
    [SerializeField] EnergyRegenType regenType;
    int startEnergy = 0;
    // Start is called before the first frame update
    void Awake()
    {
        ps = this.gameObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTurnStart() //handles on turn start setup for it's assigned player
    {
        regenEnergy();
    }

    public virtual void regenEnergy()
    {
        switch (regenType)
        {
            case EnergyRegenType.Incremental:
                if(startEnergy < ps.getMaxEnergy())
                {
                    startEnergy++;
                }
                else
                {
                   startEnergy = ps.getMaxEnergy();
                }
                ps.setEnergy(startEnergy);
                break;
            case EnergyRegenType.MaxEnergy:
                ps.setEnergy(ps.getMaxEnergy());
                break;
            default:
                Debug.Log(regenType); //things that aren't defined / NoRegen
                break;
        }

        return;
    }
}
