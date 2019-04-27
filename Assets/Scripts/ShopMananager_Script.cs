using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMananager_Script : MonoBehaviour
{
    private PlayerInv_Script _playerInv;
    public GameObject ShopUI;
    public TextMeshProUGUI BloodBankText;
    public Button[] ShopButtons;
    private List<TextMeshProUGUI> _shopButtonsTexts;

    [Serializable]
    public class AllItems
    {
        public string ItemName;
        [TextArea]
        public string ItemFlavourText;
        public int ItemID;
        public int ItemCost;
        public int ItemAmountGiven;
        public GameObject ItemPrefab;
        public int ItemDropAmount;
    }

    public AllItems[] ItemsForSale;

    public int StoreBloodAmount;
    public int StoredBlood;

	void Start ()
	{
	    _playerInv = FindObjectOfType<PlayerInv_Script>();
	    ShopUI.SetActive(false);
	    UpdateBloodBankUI();
        _shopButtonsTexts = new List<TextMeshProUGUI>();
	    SetUpButtons();
    }

    public void Item1()
    {
        if (StoredBlood >= ItemsForSale[0].ItemCost)
        {
            //test seed
            _playerInv.Seeds[0].SeedCount += ItemsForSale[0].ItemAmountGiven;
            _playerInv.Seeds[0].SeedName = ItemsForSale[0].ItemName;
            _playerInv.Seeds[0].SeedDrop = ItemsForSale[0].ItemPrefab;
            _playerInv.Seeds[0].SeeDropAmount = ItemsForSale[0].ItemDropAmount;
            //_playerInv.UseBlood(ItemsForSale[0].ItemCost);
            StoredBlood -= ItemsForSale[0].ItemCost;
            UpdateBloodBankUI();


        }
    }

    public void StoreBlood()
    {
        _playerInv.UseBlood(StoreBloodAmount);
        StoredBlood += StoreBloodAmount;
        UpdateBloodBankUI();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            ShopUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            ShopUI.SetActive(false);
        }
    }

    void SetUpButtons()
    {

        foreach (Button b in ShopButtons)
        {
            TextMeshProUGUI t = b.GetComponentInChildren<TextMeshProUGUI>();
            _shopButtonsTexts.Add(t);
        }

        int itemRef = 0;
        foreach (TextMeshProUGUI t in _shopButtonsTexts)
        {
            t.text = ItemsForSale[itemRef].ItemName + "\n" + ItemsForSale[itemRef].ItemCost;
            itemRef++;
        }
    }

    void UpdateBloodBankUI()
    {
        BloodBankText.text = "Blood Stored: \n" + StoredBlood + " l";
    }

}
