using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public float MaxBloodAmount = 100;
    public float BloodAmount;
    public Image BloodbagBackground;
    public TextMeshProUGUI SeedText;

    void Start()
    {
        BloodAmount = MaxBloodAmount;
        UpdateSeedText();
    }


    public void UseSeed(int id)
    {
        if (Seeds[id].SeedCount > 0)
        {
            Seeds[id].SeedCount--;
            UpdateSeedText();
        }
    }

    public void UpdateSeedText()
    {
        SeedText.text = "Seeds:\n " + Seeds[0].SeedCount;
    }

    public void UseBlood(int a)
    {
        BloodAmount -= a;
        //TO-DO: spawn some blood stain effect or somthing and slite cam shake
        UpdateBlooBag();
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
        UpdateBlooBag();
    }

    void UpdateBlooBag()
    {
        float b = BloodAmount / MaxBloodAmount;
        BloodbagBackground.fillAmount = b;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            UseBlood(10);
        }
    }
}
