using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

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
    public float FertilizeCost;

    public float MaxBloodAmount = 100;
    public float BloodAmount;
    public Image BloodbagBackground;
    public TextMeshProUGUI SeedText;
    public GameMananger_Script GameManagScript;
    public GameObject BloodDropPrefab;

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
        GameObject bd = Instantiate(BloodDropPrefab);
        bd.transform.localEulerAngles = new Vector3(0,0,0);
        
        if (BloodAmount <= 0)
        {
            Debug.Log("player dead, you bleed out");
            this.gameObject.SetActive(false);
            GameManagScript.GameOver("Game Over","You bleed out");
        }

    }

    public void GainBlood(int a)
    {
        BloodAmount += a;
        GameManagScript.TotalBlood += a;
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

    public float UpgradeFertilizeCost(float a)
    {
        FertilizeCost -= a;
        Debug.Log("Fertilize Cost: " +FertilizeCost);
        return FertilizeCost;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            UseBlood(10);
        }
    }
}
