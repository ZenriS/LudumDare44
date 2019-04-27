using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInv_Script : MonoBehaviour
{
    [Serializable]
    public class AllSeeds
    {
        public int SeedID = 1;
        public string SeedName = "Test Seed";
        public int SeedCount = 5;
    }

    public AllSeeds[] Seeds;

    public int MaxBloodAmount = 100;
    public int BloodAmount;

    void Start()
    {
        BloodAmount = MaxBloodAmount;
    }


    public void UseSeed(int id)
    {
        if (Seeds[id].SeedCount > 0)
        {
            Seeds[id].SeedCount--;
        }
    }

    public void UseBlood(int a)
    {
        BloodAmount -= a;
        //TO-DO: spawn some blood stain effect or somthing and slite cam shake
        if (BloodAmount <= 0)
        {
            Debug.Log("player dead");
        }
    }

    public void GainBlood(int a)
    {
        BloodAmount += a;
        if (BloodAmount > MaxBloodAmount)
        {
            BloodAmount = MaxBloodAmount;
        }
    }
}
