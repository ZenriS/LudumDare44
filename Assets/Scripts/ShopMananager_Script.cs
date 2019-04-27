using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMananager_Script : MonoBehaviour
{
    private PlayerInv_Script _playerInv;

    [Serializable]
    public class AllItems
    {
        public string ItemName;
        [TextArea]
        public string ItemFlavourText;
        public int ItemID;
        public int ItemCost;
        public int ItemAmountGiven;
    }

    public AllItems[] ItemsForSale;

	void Start ()
	{
	    _playerInv = FindObjectOfType<PlayerInv_Script>();
	}

    public void Item1()
    {
        //test seed
        _playerInv.Seeds[0].SeedCount += ItemsForSale[0].ItemAmountGiven;
        _playerInv.UseBlood(ItemsForSale[0].ItemCost);
    }
	

}
