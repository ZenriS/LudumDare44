using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInv_Script : MonoBehaviour
{
    [Serializable]
    public class AllSeeds
    {
        public string SeedName;
        public int SeedID;
        public GameObject SeedDrop;
        public int SeeDropAmount;
        public int SeedCount;
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
            Debug.Log("player dead, you bleed out");
            this.gameObject.SetActive(false);
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
