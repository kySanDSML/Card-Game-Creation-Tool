using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public bool isHealthWinCond; //does the health of this player win/lose the game?

    [SerializeField] int energy;
    [SerializeField] int maxEnergy;
    [SerializeField] int health;
    [SerializeField] int maxHealth;

    public int getEnergy()
    {
        return energy;
    }
    public void setEnergy(int value)
    {
        energy = value;
    }
    public void capEnergy()
    {
        if(energy > maxEnergy)
        {
            energy = maxEnergy;
        } else if(energy < 0)
        {
            energy = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
